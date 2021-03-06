﻿using QuantitativeAnalysis.ModelLayer.Common;
using QuantitativeAnalysis.ModelLayer.Option;
using QuantitativeAnalysis.ModelLayer.PositionModel;
using QuantitativeAnalysis.ServiceLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantitativeAnalysis.DataAccessLayer.DataToLocalCSV.Option
{
    public class OptionRecordUtil
    {
        public static List<HoldingStatus> Transfer(SortedDictionary<DateTime, Dictionary<string, PositionsWithDetail>> positions)
        {
            List<OptionInfo> optionInfo = (List<OptionInfo>)Caches.get("OptionInfo");
            List<HoldingStatus> myRecord = new List<HoldingStatus>();
            var positionShot = positions.Last().Value;
            foreach (var item in positionShot)
            {
                string remarks = "";
                foreach (var option in optionInfo)
                {
                    if (option.optionCode == item.Key)
                    {
                        remarks += option.optionName;
                    }
                }
                foreach (var records in item.Value.record)
                {

                    myRecord.Add(new HoldingStatus(records.time, records.code, records.price, records.volume, remarks));
                }
            }
            myRecord = myRecord.OrderBy(x => x.time).ToList();
            return myRecord;
        }
    }
}
