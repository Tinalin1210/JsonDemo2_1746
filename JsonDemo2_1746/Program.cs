﻿using System.Collections.Generic;//泛型集合類型，例如 List<T>
using System;
using Newtonsoft.Json;
using System.Xml;  //提供 XML 處理的功能
using System.IO;  //提供 檔案 (File) 和資料流 (Stream) 操作 的功能，例如讀取或寫入檔案

namespace JsonDemo2_1734
{
    public class Program
    {


        static void Main(string[] args)
        {

            GuaranteeWrapper guaranteeWrapper = new GuaranteeWrapper
            {
                SearchCriteria = new SearchCriteria
                {
                    // 設定搜尋條件中的擔保品日期
                    CollateralDate = DateTime.Now,
                    // 設定投資人帳號
                    InvestorAccount = "Tinalin1210",
                    // 設定成交狀態為成功
                    TransactionStatus = "成功",
                    // 設定還款狀態為成功
                    RepaymentStatus = "成功"
                },
                // 初始化投資人帳號群組清單
                InvestorAccountGroup = new List<InvestorAccountGroup>
                {
                    new InvestorAccountGroup
                    {
                        // 設定擔保品序號
                        GuaranteeSerialNumber = "A12345",
                        // 設定擔保品序號日期
                        GuaranteeSerialDate = DateTime.Now
                    },

                },
                // 初始化擔保資料清單
                GuaranteeData = new List<GuaranteeData>
                {
                    new GuaranteeData
                    {
                        InvestorAccount = "Tinalin1210",  //投資人帳號
                        GuaranteeDate = DateTime.Now,    //擔保日期
                        GuaranteeSerialNumber = "A12345", //序號
                        CommissionDate = DateTime.Now,    //委託日期
                        CommissionSerialNumber = "Y12345", //序號
                        ReportedGuaranteeAmount = 9000,    //已申報擔保總金額
                        GuaranteeMaintainRate = 80,        //擔保維持率
                        ExceededGuaranteeRate = 90,        //超過擔保規定比率總值
                        FunctionalCode = "HHHHH",          //功能碼
                        TransactionStatus = "成功",       //成交狀態
                        RepaymentStatus = "成功"          //還券狀態
                    }
                },
                // 初始化擔保資料詳細清單
                GuaranteeDataDetail = new List<GuaranteeDataDetail>
                {
                    new GuaranteeDataDetail
                    {
                        GuaranteeDate = DateTime.Now,    //擔保日期
                        GuaranteeSerialNumber = "A12345", //擔保序號
                        GuaranteeCategory = "A",          //擔保種類
                        BankGuaranteeSerialNumber = "AA123", //銀行保證序號
                        BankSourceOfSecurities = "台新",   //券商
                        BankSourceOfAccount = "Tinalin1210",//帳號
                        ChangeQuantityAmount = "2/40",     //異動數量/金額
                        RemainingQuantity = "10"           //剩餘數量/金額
                    }
                }
            };

            // 使用 Newtonsoft.Json 將 GuaranteeWrapper 物件序列化為 JSON 格式
            // Formatting.Indented 讓 JSON 輸出格式有縮排，便於閱讀
            string jsonData = JsonConvert.SerializeObject(guaranteeWrapper, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(jsonData);

            // 儲存序列化的 JSON 資料到檔案
            string filePath = @"guaranteeWrapper.json";
            File.WriteAllText(filePath, jsonData);

            Console.WriteLine("JSON資料已儲存至guaranteeWrapper.json");

            // 用來取得你現在程式執行的資料夾位置。
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // 目的是把你剛剛取得的資料夾路徑 (baseDirectory) 和檔案名稱 (filePath) 連在一起，產生出完整的檔案路徑
            string fullFilePath = Path.Combine(baseDirectory, filePath);

            // 檢查檔案是否存在
            if (File.Exists(fullFilePath))
            {
                // 讀取 JSON 檔案內容
                string jsonData1 = File.ReadAllText(fullFilePath);

                // 反序列化 JSON 資料回 GuaranteeWrapper 物件
                GuaranteeWrapper guaranteeWrapper1 = JsonConvert.DeserializeObject<GuaranteeWrapper>(jsonData1);

                // 輸出反序列化的資料，這裡是列出每一筆擔保資料
                foreach (var guarantee in guaranteeWrapper1.GuaranteeData)
                {
                    Console.WriteLine("投資人帳號 :" + guarantee.InvestorAccount);
                    Console.WriteLine("擔保日期 :" + guarantee.GuaranteeDate);
                    Console.WriteLine("序號 :" + guarantee.GuaranteeSerialNumber);
                    Console.WriteLine("委託日期 :" + guarantee.CommissionDate);
                    Console.WriteLine("序號 :" + guarantee.CommissionSerialNumber);
                    Console.WriteLine("已申報擔保總金額 :" + guarantee.ReportedGuaranteeAmount);
                    Console.WriteLine("擔保維持率 :" + guarantee.GuaranteeMaintainRate);
                    Console.WriteLine("超過擔保規定比率總值 :" + guarantee.ExceededGuaranteeRate);
                    Console.WriteLine("功能碼 :" + guarantee.FunctionalCode);
                    Console.WriteLine("還券狀態 :" + guarantee.RepaymentStatus);
                }
            }
            else
            {
                Console.WriteLine("找不到檔案");
            }

            Console.ReadLine();
        }
    }

    // GuaranteeWrapper 類別，用來封裝搜尋條件、投資人帳號群組、擔保資料、擔保資料細節
    public class GuaranteeWrapper
    {
        /// <summary>
        /// 搜尋條件，包含擔保品日期、投資人帳號、成交狀態、還款狀態等條件
        /// </summary>
        public SearchCriteria SearchCriteria { get; set; }

        /// <summary>
        /// 投資人帳號群組，每個群組擁有一個擔保品序號和日期
        /// </summary>
        public List<InvestorAccountGroup> InvestorAccountGroup { get; set; }

        /// <summary>
        /// 擔保資料清單，每個擔保資料包含投資人帳號、擔保日期、已申報金額等資訊
        /// </summary>
        public List<GuaranteeData> GuaranteeData { get; set; }

        /// <summary>
        /// 擔保資料的詳細清單，包含擔保種類、銀行保證序號等細節
        /// </summary>
        public List<GuaranteeDataDetail> GuaranteeDataDetail { get; set; }
    }

    // GuaranteeData 類別，包含每筆擔保資料的詳細資訊
    public class GuaranteeData
    {
        /// <summary>
        /// 投資人帳號
        /// </summary>
        public string InvestorAccount { get; set; }

        /// <summary>
        /// 擔保日期
        /// </summary>
        public DateTime GuaranteeDate { get; set; }

        /// <summary>
        /// 擔保序號
        /// </summary>
        public string GuaranteeSerialNumber { get; set; }

        /// <summary>
        /// 委託日期
        /// </summary>
        public DateTime CommissionDate { get; set; }

        /// <summary>
        /// 委託序號
        /// </summary>
        public string CommissionSerialNumber { get; set; }

        /// <summary>
        /// 已申報擔保總金額
        /// </summary>
        public decimal ReportedGuaranteeAmount { get; set; }

        /// <summary>
        /// 擔保維持率
        /// </summary>
        public decimal GuaranteeMaintainRate { get; set; }

        /// <summary>
        /// 超過擔保規定比率總值
        /// </summary>
        public decimal ExceededGuaranteeRate { get; set; }

        /// <summary>
        /// 功能碼
        /// </summary>
        public string FunctionalCode { get; set; }

        /// <summary>
        /// 成交狀態
        /// </summary>
        public string TransactionStatus { get; set; }

        /// <summary>
        /// 還款狀態
        /// </summary>
        public string RepaymentStatus { get; set; }
    }

    // SearchCriteria 類別，包含搜尋的擔保品日期、投資人帳號、成交狀態、還款狀態等條件
    public class SearchCriteria
    {
        /// <summary>
        /// 擔保品日期
        /// </summary>
        public DateTime CollateralDate { get; set; }

        /// <summary>
        /// 投資人帳號
        /// </summary>
        public string InvestorAccount { get; set; }

        /// <summary>
        /// 成交狀態
        /// </summary>
        public string TransactionStatus { get; set; }

        /// <summary>
        /// 還款狀態
        /// </summary>
        public string RepaymentStatus { get; set; }
    }

    // InvestorAccountGroup 類別，包含投資人帳號群組的擔保品序號與日期
    public class InvestorAccountGroup
    {
        /// <summary>
        /// 擔保品序號
        /// </summary>
        public string GuaranteeSerialNumber { get; set; }

        /// <summary>
        /// 擔保品序號日期
        /// </summary>
        public DateTime GuaranteeSerialDate { get; set; }
    }

    // GuaranteeDataDetail 類別，包含每筆擔保資料的細節，例如擔保種類、證券來源等
    public class GuaranteeDataDetail
    {
        /// <summary>
        /// 擔保日期
        /// </summary>
        public DateTime GuaranteeDate { get; set; }

        /// <summary>
        /// 擔保序號
        /// </summary>
        public string GuaranteeSerialNumber { get; set; }

        /// <summary>
        /// 擔保種類
        /// </summary>
        public string GuaranteeCategory { get; set; }

        /// <summary>
        /// 銀行保證序號
        /// </summary>
        public string BankGuaranteeSerialNumber { get; set; }

        /// <summary>
        /// 券商名稱
        /// </summary>
        public string BankSourceOfSecurities { get; set; }

        /// <summary>
        /// 券商帳號
        /// </summary>
        public string BankSourceOfAccount { get; set; }

        /// <summary>
        /// 異動數量/金額
        /// </summary>
        public string ChangeQuantityAmount { get; set; }

        /// <summary>
        /// 剩餘數量/金額
        /// </summary>
        public string RemainingQuantity { get; set; }
    }
}