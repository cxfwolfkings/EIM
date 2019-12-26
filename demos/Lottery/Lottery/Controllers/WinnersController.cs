using Lottery.Common;
using Lottery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Lottery.Controllers
{
    public class WinnersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = int.MaxValue;
            var rootDir = HostingEnvironment.MapPath("~/App_Data/");
            var dic = new Dictionary<string, string>();
            if (System.IO.File.Exists(rootDir + "winners.json"))
            {
                dic = JsonHelper.ReadFromJson(rootDir + "winners.json");
            }
            if (dic.ContainsKey(id + ""))
            {
                return js.Serialize(new
                {
                    error = "已抽过奖！"
                });
            }
            var users = CsvHelper.CsvToList(rootDir + "Employees.csv");
            // 排除已中奖用户
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
            switch (id)
            {
                case 1:
                    indexes = GetIndex(1, users.Count);
                    break;
                case 2:
                    indexes = GetIndex(3, users.Count);
                    break;
                case 3:
                    indexes = GetIndex(5, users.Count);
                    break;
                case 4:
                    indexes = GetIndex(10, users.Count);
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
            dic.Add(id + "", value);
            JsonHelper.SaveToJson(dic, rootDir + "winners.json");
            return js.Serialize(result);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
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