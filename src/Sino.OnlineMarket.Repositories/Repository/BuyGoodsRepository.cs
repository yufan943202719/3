using Sino.OnlineMarket.Repositories.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sino.OnlineMarket.Repositories.Repository
{
    public class BuyGoodsRepository
    {
        private OnlineMarketContext DB = new OnlineMarketContext();


        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="buyGoods"></param>
        /// <returns></returns>
        public async Task<int> AddBuyGoods(BuyGoods buyGoods)
        {
            int count = 0;
            try
            {
                await Task.Run(() =>
                {
                    DB.Add(buyGoods);
                    count = DB.SaveChanges();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());
            }
            return count;
        }

       /// <summary>
       /// 购买单个商品
       /// </summary>
       /// <param name="Userid">用户编号</param>
       /// <param name="Goodsid">商品编码</param>
       /// <returns></returns>
        public async Task<int> AlterBuyGoodsSingle(int Userid, string Goodsid)
        {
            int count = 0;
            try
            {

                var b = DB.BuyGoods.FirstOrDefault(t => t.UserId == Userid && t.GoodsId == Goodsid);


                if(b.BuyStatus == 1)
                {
                    b.BuyDateTime = DateTime.Now;
                    b.BuyStatus = 2;
                    DB.BuyGoods.Update(b);

                    await Task.Run(() =>
                    {
                        count = DB.SaveChanges();

                    });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());
            }
            return count;
        }

        /// <summary>
        /// 购买全部商品
        /// </summary>
        /// <param name="Userid">用户编号</param>
        /// <returns></returns>
        public async Task<int> AlterBuyGoodsAll(int Userid)
        {
            int count = 0;
            try
            {

                var buy = DB.BuyGoods.Where(t => t.UserId == Userid).ToList();

                foreach (var b in buy)
                {
                    if (b.BuyStatus == 1)
                    {
                        b.BuyDateTime = DateTime.Now;
                        b.BuyStatus = 2;
                        DB.BuyGoods.Update(b);

                        await Task.Run(() =>
                        {
                            count = DB.SaveChanges();

                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());
            }
            return count;
        }

        /// <summary>
        /// 显示购物车的商品
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public async Task<List<BuyGoods>> GetGoodsFromCar(int userid)
        {
            List<BuyGoods> b = new List<BuyGoods>();
            var buygoodslist = DB.BuyGoods.ToList();
            try
            {
                await Task.Run(() =>
                {
                    b = buygoodslist.Where(t => t.UserId == userid&&t.BuyStatus==1).ToList();


                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());

            }
            return b;

        }


        /// <summary>
        /// 显示已购商品记录
        /// </summary>
        /// <param name="userid">用户编号</param>
        /// <returns></returns>
        public async Task<List<BuyGoods>> GetBuyGoods(int userid)
        {
            List<BuyGoods> b = new List<BuyGoods>();
            var buygoodslist = DB.BuyGoods.ToList();
            try
            {
                await Task.Run(() =>
                {
                    b = buygoodslist.Where(t => t.UserId == userid && t.BuyStatus == 2).ToList();


                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());

            }
            return b;

        }


        /// <summary>
        /// 删除购物车中单个商品
        /// </summary>
        /// <param name="Userid">用户编号</param>
        /// <param name="Goodsid">商品编号</param>
        /// <returns></returns>
        public async Task<int> DeleteBuyGoodsSingle(int Userid, string Goodsid)
        {
            int count = 0;
            try
            {
                await Task.Run(() =>
                {
                    var b = DB.BuyGoods.FirstOrDefault(t => string.Equals(t.GoodsId, Goodsid) && t.UserId == Userid && t.BuyStatus == 1);

                    DB.BuyGoods.Remove(b);
                    count = DB.SaveChanges();

                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());
            }
            return count;
        }


        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="Userid">用户编号</param>
        /// <returns></returns>
        public async Task<int> DeleteBuyGoodsAll(int Userid)
        {
            int count = 0;
            List<BuyGoods> b = new List<BuyGoods>();
            var buygoodslist = DB.BuyGoods.ToList();
            try
            {
                await Task.Run(() =>
                {
                    b = buygoodslist.Where(t => t.UserId == Userid && t.BuyStatus == 1).ToList();

                    DB.BuyGoods.RemoveRange(b);
                    count = DB.SaveChanges();

                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("获取错误信息：{0}", ex.Message.ToString());
            }
            return count;
        }

    }
}
