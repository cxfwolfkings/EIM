using Lottery.Common;
using Lottery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Lottery.Controllers
{
    public class LotteryController : Controller
    {
        private string RootDir = HostingEnvironment.MapPath("~/App_Data/");

        // GET: Lottery
        public ActionResult Index(int? Level)
        {
            Level = Level.HasValue ? Level.Value : 5;
            return View(Level);
        }

        public ActionResult Start(int Level)
        {
            if (System.IO.File.Exists(RootDir + "Employees.csv"))
            {
                var dic = new Dictionary<string, string>();
                if (System.IO.File.Exists(RootDir + "winners.json"))
                {
                    dic = JsonHelper.ReadFromJson(RootDir + "winners.json");
                }
                if (Level > 1 && dic.ContainsKey(Level + ""))
                {
                    throw new Exception("已抽过奖！");
                }
                return Json(new
                {
                    Status = (int)HttpStatusCode.OK
                }, JsonRequestBehavior.AllowGet);
            }
            throw new Exception("请先导入用户！");
        }

        public ActionResult Clear()
        {
            if (System.IO.File.Exists(RootDir + "winners.json"))
            {
                System.IO.File.Delete(RootDir + "winners.json");
            }
            return Json(new
            {
                Status = (int)HttpStatusCode.OK,
                Msg = "清除成功！"
            });
        }

        public ActionResult Selector(int Level)
        {
            var dic = new Dictionary<string, string>();
            if (System.IO.File.Exists(RootDir + "winners.json"))
            {
                dic = JsonHelper.ReadFromJson(RootDir + "winners.json");
            }
            if (Level > 1 && dic.ContainsKey(Level + ""))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Win(int Level)
        {
            var dic = new Dictionary<string, string>();
            var users = CsvHelper.CsvToList(RootDir + "Employees.csv");
            // 排除已中奖用户
            if (System.IO.File.Exists(RootDir + "winners.json"))
            {
                dic = JsonHelper.ReadFromJson(RootDir + "winners.json");
            }
            if (dic.Count > 0)
            {
                foreach (var key in dic.Keys)
                {
                    var winner = dic[key];
                    var winnerNos = winner.Split(';').Select(_ => _.Split(':')[0]).ToList();
                    users = users.Where(_ => !winnerNos.Contains(_.UserNo)).ToList();
                }
            }
            HashSet<int> indexes = null;
            switch (Level)
            {
                case 0:
                case 1:
                    if (Level == 1)
                    {
                        // 排除黑名单用户
                        var blackDic = new Dictionary<string, string>();
                        if (System.IO.File.Exists(RootDir + "blacklist.json"))
                        {
                            blackDic = JsonHelper.ReadFromJson(RootDir + "blacklist.json");
                        }
                        if (blackDic.Count > 0)
                        {
                            var blacklist = blackDic["User"];
                            var blackNos = blacklist.Split(',').ToList();
                            users = users.Where(_ => !blackNos.Contains(_.UserNo)).ToList();
                        }
                    }
                    indexes = GetIndex(1, users.Count);
                    break;
                case 2:
                    indexes = GetIndex(5, users.Count);
                    break;
                case 3:
                    indexes = GetIndex(10, users.Count);
                    break;
                case 4:
                    indexes = GetIndex(15, users.Count);
                    break;
                case 5:
                    indexes = GetIndex(20, users.Count);
                    break;
                default:
                    throw new Exception("参数不对！");
            }
            var result = indexes.Select(_ => new User
            {
                UserNo = users[_].UserNo,
                UserName = users[_].UserName
            }).ToList();
            string value = string.Join(";", result.Select(_ => _.UserNo + ":" + _.UserName).ToArray());
            if (dic.ContainsKey(Level + ""))
            {
                dic[Level + ""] = dic[Level + ""] + ";" + value;
            }
            else
            {
                dic.Add(Level + "", value);
            }
            JsonHelper.SaveToJson(dic, RootDir + "winners.json");
            return Json(new
            {
                Status = (int)HttpStatusCode.OK,
                Data = result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Turntable()
        {
            int count;
            if (System.IO.File.Exists(RootDir + "setting.json"))
            {
                Dictionary<string, string> dic = JsonHelper.ReadFromJson(RootDir + "setting.json");
                count = Convert.ToInt32(dic.ContainsKey("TableCount") ? dic["TableCount"] : "0");
            }
            else
            {
                throw new Exception("请先设置桌数！");
            }
            return View(count);
        }

        public ActionResult TurntableShadow()
        {
            int count;
            if (System.IO.File.Exists(RootDir + "setting.json"))
            {
                Dictionary<string, string> dic = JsonHelper.ReadFromJson(RootDir + "setting.json");
                count = Convert.ToInt32(dic.ContainsKey("TableCount") ? dic["TableCount"] : "0");
            }
            else
            {
                throw new Exception("请先设置桌数！");
            }
            return View(count);
        }

        public ActionResult Versus()
        {
            return View();
        }

        public ActionResult VersusShadow()
        {
            return View();
        }

        public ActionResult Price()
        {
            var dic = new Dictionary<string, string>();
            if (System.IO.File.Exists(RootDir + "winners.json"))
            {
                dic = JsonHelper.ReadFromJson(RootDir + "winners.json");
            }
            return View(dic);
        }

        public ActionResult Setting()
        {
            var dic = new Dictionary<string, string>();
            if (System.IO.File.Exists(RootDir + "setting.json"))
            {
                dic = JsonHelper.ReadFromJson(RootDir + "setting.json");
            }
            var users = new List<User>();
            if (System.IO.File.Exists(RootDir + "Employees.csv"))
            {
                users = CsvHelper.CsvToList(RootDir + "Employees.csv");
            }
            return View(new SettingModel
            {
                Count = dic.ContainsKey("TableCount") ? dic["TableCount"] : "",
                Users = users
            });
        }

        public ActionResult Luckytable()
        {
            int count;
            if (System.IO.File.Exists(RootDir + "setting.json"))
            {
                Dictionary<string, string> dic = JsonHelper.ReadFromJson(RootDir + "setting.json");
                count = Convert.ToInt32(dic.ContainsKey("TableCount") ? dic["TableCount"] : "0");
            }
            else
            {
                throw new Exception("请先设置桌数！");
            }
            return View(count);
        }

        public ActionResult LuckytableShadow()
        {
            int count;
            if (System.IO.File.Exists(RootDir + "setting.json"))
            {
                Dictionary<string, string> dic = JsonHelper.ReadFromJson(RootDir + "setting.json");
                count = Convert.ToInt32(dic.ContainsKey("TableCount") ? dic["TableCount"] : "0");
            }
            else
            {
                throw new Exception("请先设置桌数！");
            }
            return View(count);
        }

        /// <summary>
        /// 保存桌数
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(int? Count)
        {
            Dictionary<string, string> dicResult = new Dictionary<string, string>();
            dicResult.Add("TableCount", Count.ToString());
            JsonHelper.SaveToJson(dicResult, RootDir + "setting.json");
            return Json(new
            {
                Status = (int)HttpStatusCode.OK
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 复制用户
        /// </summary>
        /// <returns></returns>
        public ActionResult Import(IList<User> Users)
        {
            CsvHelper.ListToCSV(Users, RootDir + "Employees.csv", false);
            return Json(new
            {
                Status = (int)HttpStatusCode.OK
            }, JsonRequestBehavior.AllowGet);
        }

        private HashSet<int> GetIndex(int count, int total)
        {
            HashSet<int> set = new HashSet<int>();
            Random r = new Random();
            while (set.Count < count)
            {
                set.Add(r.Next(0, total));
            }
            return set;
        }
    }
}