using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Net.Http;
using FootballRanking.ViewModels;
using FootballModels;
using FootballDataAccess.Repository.IRepository;
using Constants;
using FootballRanking.Extensions;

namespace FootballRanking.Controllers.Crawler
{
    [Route("api/admin")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly String Url_image = CT.URL_IMAGE;
        public CrawlerController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task CrawlerDetail(HtmlDocument htmlDocument, String crawl, String detailAnchor)
        {

            //var listDetail = HttpContext.Session.GetObject<List<String>>(CT.DETAIL);
            var part = new List<String>();
            if (part != null)
            {
                if (crawl != null)
                {
                    foreach (var include in crawl.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        part.Add(include);
                    }
                }


                //var url = detailAnchor;
                //using var httpClient = new HttpClient();
                //var html = await httpClient.GetStringAsync(url);
                //var htmlDocument = new HtmlDocument();
                //htmlDocument.LoadHtml(html);

                var playerProfile = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("playerprofile-itemblock")).FirstOrDefault();

                //price
                var getImagePlayer = Url_image + playerProfile.Descendants("div")
                     .Where(node => node.GetAttributeValue("class", "")
                     .Contains("card-20-face-"))
                     .FirstOrDefault()
                     .Descendants("img")
                     .FirstOrDefault()
                     .Attributes["src"].Value;
                var getPlayer = unitOfWork.Player.GetFirstOrDefault(x => x.Face.Equals(getImagePlayer));
                if (getPlayer.Nation == null)
                {
                    getPlayer.Nation = htmlDocument.DocumentNode.Descendants("div")
                                       .Where(node => node.GetAttributeValue("class", "")
                                       .Equals("playerprofile-hbar-ttl")).FirstOrDefault().Descendants("a").FirstOrDefault().InnerText;
                }

                if (playerProfile != null)
                {
                    var priceObject = new TradingMarket();
                    var priceUpdate = playerProfile.Descendants("div").Where(node => node.GetAttributeValue("class", "")
                    .Contains("priceupdate"))
                    .ToList();
                    var xboxUpdate = priceUpdate[0].InnerText;
                    var ps4Update = priceUpdate[1].InnerText;
                    priceObject.Player = getPlayer.Id;
                    priceObject.Xtime = xboxUpdate;
                    priceObject.Ps4time = ps4Update;

                    var priceProfile = playerProfile.Descendants("div").Where(node => node.GetAttributeValue("class", "")
                    .Equals("playerprofile-price"))
                    .ToList();
                    var xboxPrice = priceProfile[0].InnerText;
                    var ps4Price = priceProfile[1].InnerText;
                    priceObject.Xprice = Double.Parse(xboxPrice);
                    priceObject.Ps4price = Double.Parse(ps4Price);
                    unitOfWork.TradingMarket.Add(priceObject);
                }

                //skill
                var playerInfor = htmlDocument.DocumentNode.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("ppdb-d")).ToList();
                var skillPoint = playerInfor[0].InnerText;
                var weakFoot = playerInfor[1].InnerText;
                var age = playerInfor[2].InnerText;
                var height = playerInfor[3].InnerText;
                var weight = playerInfor[4].InnerText;
                var workrates = playerInfor[5].InnerText;
                var footed = playerInfor[6].InnerText;
                var player = getPlayer.Id;
                var skillObject = new SkillDetail()
                {
                    Age = int.Parse(age),
                    Player = player,
                    Footed = footed,
                    Height = height,
                    Weight = weight,
                    Skill = int.Parse(skillPoint),
                    Weakfoot = int.Parse(weakFoot),
                    Workrates = workrates
                };
                unitOfWork.SkillDetail.Add(skillObject);
                //fight for the badge
                var badge = playerProfile.Descendants("div")
                    .Where(node => node.GetAttributeValue("align", "")
                    .Equals("center")).FirstOrDefault();
                var yes = badge.Descendants("a").FirstOrDefault().InnerText;
                var no = badge.Descendants("a").Skip(1).Take(1).FirstOrDefault().InnerText;

                var getStats = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("col-2")).ToList();
                //PacDetail
                var pacPoint = getStats[1].InnerText;
                var splitPacPoint = pacPoint.Replace("\n", "");
                var getPacPointOnly = splitPacPoint.Where(x=>char.IsDigit(x)).ToList();
                var pacObject = new PacDetail();
               // var item = getPacPointOnly[0].ToString();
                pacObject.Pac = int.Parse(getPacPointOnly[0].ToString() + getPacPointOnly[1].ToString());
                pacObject.Acc = int.Parse(getPacPointOnly[2].ToString() + getPacPointOnly[3].ToString());
                pacObject.Speed = int.Parse(getPacPointOnly[4].ToString() + getPacPointOnly[5].ToString());
                pacObject.Player = getPlayer.Id;
                unitOfWork.PacDetail.Add(pacObject);
                //ShoDetail 
                var showPoint = getStats[2].InnerText;
                var splitShoPoint = showPoint.Replace("\n", "");
                var getShoPointOnly = splitShoPoint.Where(x => char.IsDigit(x)).ToList();
                var shoObject = new ShoDetail();
                getShoPointOnly.RemoveRange(0, 2);
                shoObject.Pos = int.Parse(getShoPointOnly[0].ToString() + getShoPointOnly[1].ToString());
                shoObject.Fin = int.Parse(getShoPointOnly[2].ToString() + getShoPointOnly[3].ToString());
                shoObject.Sp = int.Parse(getShoPointOnly[4].ToString() + getShoPointOnly[5].ToString());
                shoObject.Ls = int.Parse(getShoPointOnly[6].ToString() + getShoPointOnly[7].ToString());
                shoObject.Volleys = int.Parse(getShoPointOnly[8].ToString() + getShoPointOnly[9].ToString());
                shoObject.Pen = int.Parse(getShoPointOnly[10].ToString() + getShoPointOnly[11].ToString());
                shoObject.Player = getPlayer.Id;

                unitOfWork.ShoDetail.Add(shoObject);

                //PasDetail
                var pasPoint = getStats[3].InnerText;
                var splitPasPoint = pasPoint.Replace("\n", "");
                var getPasPointOnly = splitPasPoint.Where(x => char.IsDigit(x)).ToList();
                var pasDetail = new PasDetail();
                getPasPointOnly.RemoveRange(0, 2);
                pasDetail.Vision = int.Parse(getPasPointOnly[0].ToString() + getPasPointOnly[1].ToString());
                pasDetail.Crossing = int.Parse(getPasPointOnly[2].ToString() + getPasPointOnly[3].ToString());
                pasDetail.Fk = int.Parse(getPasPointOnly[4].ToString() + getPasPointOnly[5].ToString());
                pasDetail.Sp = int.Parse(getPasPointOnly[6].ToString() + getPasPointOnly[7].ToString());
                pasDetail.Lp = int.Parse(getPasPointOnly[8].ToString() + getPasPointOnly[9].ToString());
                pasDetail.Curve = int.Parse(getPasPointOnly[10].ToString() + getPasPointOnly[11].ToString());
                pasDetail.Player = getPlayer.Id;

                unitOfWork.PasDetail.Add(pasDetail);


                //PhyDetail
                var phyPoint = getStats[6].InnerText;
                var splitPhyPoint = phyPoint.Replace("\n", "");
                var getPhyPointOnly = splitPhyPoint.Where(x => char.IsDigit(x)).ToList();
                var phyDetail = new PhyDetail();
                phyDetail.Jump = int.Parse(getPhyPointOnly[2].ToString() + getPhyPointOnly[3].ToString());
                phyDetail.Stamina = int.Parse(getPhyPointOnly[4].ToString() + getPhyPointOnly[5].ToString());
                phyDetail.Strength = int.Parse(getPhyPointOnly[6].ToString() + getPhyPointOnly[7].ToString());
                phyDetail.Aggression = int.Parse(getPhyPointOnly[8].ToString() + getPhyPointOnly[9].ToString());
                phyDetail.Player = getPlayer.Id;

                unitOfWork.PhyDetail.Add(phyDetail);


                //DefDetail
                var defPoint = getStats[5].InnerText;
                var splitDefPoint = defPoint.Replace("\n", "");
                var getDefPointOnly = splitDefPoint.Where(x => char.IsDigit(x)).ToList();
                var defDetail = new DefDetail();
                getDefPointOnly.RemoveRange(0, 2);
                defDetail.Interception = int.Parse(getDefPointOnly[0].ToString() + getDefPointOnly[1].ToString());
                defDetail.Ha = int.Parse(getDefPointOnly[2].ToString() + getDefPointOnly[3].ToString());
                defDetail.Da = int.Parse(getDefPointOnly[4].ToString() + getDefPointOnly[5].ToString());
                defDetail.Stand = int.Parse(getDefPointOnly[6].ToString() + getDefPointOnly[7].ToString());
                defDetail.Slide = int.Parse(getDefPointOnly[8].ToString() + getDefPointOnly[9].ToString());
                defDetail.Player = getPlayer.Id;

                unitOfWork.DefDetail.Add(defDetail);


                //DriDetail
                var driPoint = getStats[4].InnerText;
                var splitDriPoint = driPoint.Replace("\n", "");
                var getDriPointOnly = splitDriPoint.Where(x => char.IsDigit(x)).ToList();
                var driDetail = new DriDetail();
                getDefPointOnly.RemoveRange(0, 2);
                driDetail.Agility = int.Parse(getDriPointOnly[0].ToString() + getDriPointOnly[1].ToString());
                driDetail.Balance = int.Parse(getDriPointOnly[2].ToString() + getDriPointOnly[3].ToString());
                driDetail.React = int.Parse(getDriPointOnly[4].ToString() + getDriPointOnly[5].ToString());
                driDetail.Control = int.Parse(getDriPointOnly[6].ToString() + getDriPointOnly[7].ToString());
                driDetail.Drib = int.Parse(getDriPointOnly[8].ToString() + getDriPointOnly[9].ToString());
                driDetail.Composure = int.Parse(getDriPointOnly[10].ToString() + getDriPointOnly[11].ToString());
                driDetail.Player = getPlayer.Id;

                unitOfWork.DriDetail.Add(driDetail);

                //Position
                var position = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("altpos-inner-l1")).ToList();
                var positionPoint = htmlDocument.DocumentNode.Descendants("div")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Equals("altpos-inner-l2")).ToList();
                var positionObject = new Position();
                positionObject.Cb = int.Parse(position[0].InnerText);
                positionObject.Cdm = int.Parse(position[1].InnerText);
                positionObject.Cm = int.Parse(position[2].InnerText);
                positionObject.Cam = int.Parse(position[3].InnerText);
                positionObject.Cf = int.Parse(position[4].InnerText);
                positionObject.St = int.Parse(position[5].InnerText);
                positionObject.Lw = int.Parse(position[6].InnerText);
                positionObject.Lf = int.Parse(position[7].InnerText);
                positionObject.Rf = int.Parse(position[8].InnerText);
                positionObject.Rw = int.Parse(position[9].InnerText);
                positionObject.Lm = int.Parse(position[10].InnerText);
                positionObject.Rm = int.Parse(position[11].InnerText);
                positionObject.Rb = int.Parse(position[12].InnerText);
                positionObject.Rwb = int.Parse(position[13].InnerText);
                positionObject.Lb = int.Parse(position[14].InnerText);
                positionObject.Lwb = int.Parse(position[15].InnerText);
                positionObject.Player = getPlayer.Id;

                unitOfWork.Position.Add(positionObject);
                unitOfWork.Save();

            }


        }

        [HttpGet("crawler")]
        public async Task<IActionResult> Crawler([FromQuery] String part)
        {
            var listDetails = new List<String>();
            var anchorDetail = "";
            for (int i = 1; i < 2; i++)
            {
                var url = "https://www.futwiz.com/en/fifa20/players?page=" + i;
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var divs = htmlDocument.DocumentNode.Descendants("tr")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("table-row")).ToList();
                if (divs.Count != 0)
                {
                    //var listPlayer = new List<FootballModels.Player>();
                    foreach (var item in divs)
                    {
                        if (item.Descendants("b").FirstOrDefault() != null)
                        {
                            var team = new Team();
                            var face = item.Descendants("img").Where(x => x.GetAttributeValue("class", "").Equals("player-img")).FirstOrDefault().Attributes["src"].Value;
                            anchorDetail = Url_image + item.Descendants("td").Where(x => x.GetAttributeValue("class", "").Equals("face")).FirstOrDefault().Element("a").Attributes["href"].Value;
                            listDetails.Add(anchorDetail);
                            if (unitOfWork.Player.GetFirstOrDefault(x => x.Face.Equals(face)) == null)
                            {
                                var getStars = item.Descendants("span").Where(x => x.GetAttributeValue("class", "").Equals("starRating")).ToList();
                                var getElementByElementP = item.Descendants("p").Where(node => node.GetAttributeValue("class", "").Equals("team")).FirstOrDefault();
                                var getTeam = getElementByElementP.Descendants("a").FirstOrDefault();
                                if (getTeam != null)
                                {
                                    team = new Team()
                                    {
                                        Name = getTeam.InnerText,
                                        League = item.Descendants("a").Where(node => node.InnerText.Length != 0).Skip(2).FirstOrDefault().InnerText
                                    };
                                    var getExistTeam = unitOfWork.Team.GetFirstOrDefault(x => x.League == team.League && x.Name == team.Name);
                                    if (getExistTeam == null)
                                    {
                                        unitOfWork.Team.Add(team);
                                        unitOfWork.Save();
                                    }
                                    else
                                    {
                                        team = getExistTeam;
                                    }
                                }

                                var season = "";
                                var getSeason = item.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("otherversion18")).FirstOrDefault();

                                if (getSeason.GetAttributeValue("class", "").Contains("icon"))
                                {
                                    season = "ICO";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("totgsucl"))
                                {
                                    season = "TOTGSUCL";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("gold-inform"))
                                {
                                    season = "GI";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("otw"))
                                {
                                    season = "OTW";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("gold"))
                                {
                                    season = "G";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("scream"))
                                {
                                    season = "SC";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("sbcprem"))
                                {
                                    season = "SBC";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("potmligue1"))
                                {
                                    season = "LIG1";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("ucllive"))
                                {
                                    season = "UCL";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("champsrare"))
                                {
                                    season = "CS";
                                }
                                else if (getSeason.GetAttributeValue("class", "").Contains("potmbundes"))
                                {
                                    season = "DES";
                                }

                                var flag = item.Descendants("img").Where(x => x.GetAttributeValue("class", "").Equals("nation")).FirstOrDefault().Attributes["src"].Value;
                                var getDetails = item.Descendants("span").Where(x => x.GetAttributeValue("class", "").Equals("stat")).ToList();
                                var player = new FootballModels.Player
                                {
                                    Name = item.Descendants("b").FirstOrDefault().InnerText,
                                    Ovr = int.Parse(item.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("otherversion18-txt")).FirstOrDefault().InnerText),
                                    Pos = item.Descendants("td").Skip(3).FirstOrDefault().InnerText.Trim(),
                                    Pac = int.Parse(getDetails[0].InnerText),
                                    Sho = int.Parse(getDetails[1].InnerText),
                                    Dri = int.Parse(getDetails[3].InnerText),
                                    Def = int.Parse(getDetails[4].InnerText),
                                    Phy = int.Parse(getDetails[5].InnerText),
                                    Sm = int.Parse(getStars[0].InnerText),
                                    Wf = int.Parse(getStars[1].InnerText),
                                    Pas = int.Parse(getDetails[2].InnerText),
                                    Wrs = item.Descendants("td").Skip(13).FirstOrDefault().InnerText,
                                    Foot = item.Descendants("td").Skip(14).FirstOrDefault().InnerText,
                                    Stats = int.Parse(item.Descendants("td").Skip(15).FirstOrDefault().InnerText.Replace(",", "")),
                                    Team = team.Id,
                                    Season = season,
                                    Face = Url_image + face,
                                    Flag = Url_image + flag
                                };
                                unitOfWork.Player.Add(player);

                                //CrawlerDetail(null, anchorDetail);
                            }
                        }
                    }
                    unitOfWork.Save();
                    await TestCrawl(listDetails);
                    // HttpContext.Session.SetObject(CT.DETAIL, listDetails);
                    return Ok();
                }
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> TestCrawl(List<String> listDetails)
        {
            //listDetails.Add("https://www.futwiz.com/en/fifa20/player/pele/271");
            foreach (var item in listDetails)
            {
                var url = item;
                using var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                await CrawlerDetail(htmlDocument, null, item);
            }

            return Ok();
        }
        [HttpGet("test")]
        public async Task<IActionResult> TestCrawl2()
        {
            var listDetail = new List<String>();
            listDetail.Add("https://www.futwiz.com/en/fifa20/player/pele/271");
            foreach (var item in listDetail)
            {
                var url = item;
                using var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                await CrawlerDetail(htmlDocument, null, item);
            }

            return Ok();
        }
    }
}