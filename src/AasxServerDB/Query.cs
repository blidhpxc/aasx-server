﻿using Extensions;
using Microsoft.IdentityModel.Tokens;

namespace AasxServerDB
{
    // --------------- Result Schema ---------------
    public class SMResult
    {
        public string smId { get; set; }
        public string url { get; set; }
    }

    public class SMEResult
    {
        public string smId { get; set; }
        public string idShortPath { get; set; }
        public string value { get; set; }
        public string url { get; set; }
    }

    // --------------- Query ---------------
    public class Query
    {
        public static string ExternalBlazor { get; set; }

        // --------------- API ---------------
        public List<SMResult> SearchSMs(string semanticId = "")
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine("SearchSMs");
            Console.WriteLine("Total number of SMs " + (new AasContext()).SMSets.Count() + " in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            var smList = getSMSet(semanticId);
            Console.WriteLine("Found " + smList.Count() + " SM in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            List<SMResult> list = getSMResult(smList);
            Console.WriteLine("Collected result in " + watch.ElapsedMilliseconds + "ms");

            return list;
        }

        public int CountSMs(string semanticId = "")
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine("CountSMs");
            Console.WriteLine("Total number of SMs " + new AasContext().SMSets.Count() + " in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            var smList = getSMSet(semanticId);
            int count = smList.Count();
            Console.WriteLine("Found " + count + " SM in " + watch.ElapsedMilliseconds + "ms");

            return count;
        }

        public List<SMEResult> SearchSMEs(
            string smSemanticId = "", string semanticId = "",
            string equal = "", string lower = "", string upper = "", string contains = "")
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine("SearchSMEs");
            Console.WriteLine("Total number of SMEs " + new AasContext().SMESets.Count() + " in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            bool withSmId = !smSemanticId.IsNullOrEmpty();
            List<SMSet> smList = new List<SMSet>();
            if (withSmId)
                smList = getSMSet(smSemanticId);
            List<SMESet> smeList = getSMESet(semanticId);
            List<SValueSet> sValueList = getSValueSet(contains, equal);
            List<IValueSet> iValueList = new List<IValueSet>();
            List<DValueSet> dValueList = new List<DValueSet>();
            if (contains.IsNullOrEmpty())
            {
                iValueList = getIValueSet(equal, lower, upper);
                dValueList = getDValueSet(equal, lower, upper);
            }
            List<smeWithValue> smeValue = combineSMEValue(withSmId, smList, smeList, sValueList, iValueList, dValueList);
            Console.WriteLine("Found " + smeValue.Count() + " SMEs in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            List<SMEResult> result = getSMEResult(smeValue);
            Console.WriteLine("Collected result in " + watch.ElapsedMilliseconds + "ms");

            return result;
        }
        
        public int CountSMEs(
            string smSemanticId = "", string semanticId = "",
            string equal = "", string lower = "", string upper = "", string contains = "")

        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine("CountSMEs");
            Console.WriteLine("Total number of SMEs " + new AasContext().SMESets.Count() + " in " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            bool withSmId = !smSemanticId.IsNullOrEmpty();
            List<SMSet> smList = new List<SMSet>();
            if (withSmId)
                smList = getSMSet(smSemanticId);
            List<SMESet> smeList = getSMESet(semanticId);
            List<SValueSet> sValueList = getSValueSet(contains, equal);
            List<IValueSet> iValueList = new List<IValueSet>();
            List<DValueSet> dValueList = new List<DValueSet>();
            if (contains.IsNullOrEmpty())
            {
                iValueList = getIValueSet(equal, lower, upper);
                dValueList = getDValueSet(equal, lower, upper);
            }
            List<smeWithValue> smeValue = combineSMEValue(withSmId, smList, smeList, sValueList, iValueList, dValueList);
            int count = smeValue.Count();
            Console.WriteLine("Found " + count + " SMEs in " + watch.ElapsedMilliseconds + "ms");

            return count;
        }

        public List<SMEResult> SearchSMEsResult(
                    string smSemanticId = "",
                    string searchSemanticId = "",
                    string searchIdShort = "",
                    string equal = "",
                    string contains = "",
                    string resultSemanticId = "",
                    string resultIdShort = "")
        {
            List<SMEResult> result = new List<SMEResult>();
            
            if (searchSemanticId.IsNullOrEmpty() && searchIdShort.IsNullOrEmpty())
                return result;
            if (equal.IsNullOrEmpty() && contains.IsNullOrEmpty())
                return result;
            if (resultSemanticId.IsNullOrEmpty() && resultIdShort.IsNullOrEmpty())
                return result;

            bool withI = false;
            long iEqual = 0;
            bool withF = false;
            double fEqual = 0;
            try
            {
                if (!equal.IsNullOrEmpty())
                {
                    iEqual = Convert.ToInt64(equal);
                    withI = true;
                    fEqual = Convert.ToDouble(equal);
                    withF = true;
                }
            }
            catch { }

            using (AasContext db = new AasContext())
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Console.WriteLine();
                Console.WriteLine("SearchSMEs");
                Console.WriteLine("Total number of SMEs " + db.SMESets.Count() + " in " + watch.ElapsedMilliseconds + "ms");
                watch.Restart();

                bool withContains = (!contains.IsNullOrEmpty());
                bool withEqual = !withContains && (!equal.IsNullOrEmpty());

                var list = db.SValueSets.Where(v =>
                    (withContains && v.Value.Contains(contains)) ||
                    (withEqual && v.Value == equal)
                    )
                    .Join(db.SMESets,
                        v => v.SMEId,
                        sme => sme.Id,
                        (v, sme) => new
                        {
                            SMId = sme.SMId,
                            SemanticId = sme.SemanticId,
                            IdShort = sme.IdShort,
                            ParentSme = sme.ParentSMEId,
                            Value = v.Value
                        }
                    )
                    .Where(s =>
                        (!searchSemanticId.IsNullOrEmpty() && s.SemanticId == searchSemanticId) ||
                        (!searchIdShort.IsNullOrEmpty() && s.IdShort == searchIdShort)
                    )
                    .Join(db.SMSets,
                        v => v.SMId,
                        s => s.Id,
                        (v, s) => new
                        {
                            Id = s.Id,
                            SemanticId = s.SemanticId,
                            ParentSme = v.ParentSme,
                            Value = v.Value
                        }
                    )
                    .Where(s =>
                        smSemanticId == "" || s.SemanticId == smSemanticId
                    )
                    .ToList();

                list.AddRange(db.IValueSets.Where(v =>
                    (withEqual && withI && v.Value == iEqual)
                    )
                    .Join(db.SMESets,
                        v => v.SMEId,
                        sme => sme.Id,
                        (v, sme) => new
                        {
                            SMId = sme.SMId,
                            SemanticId = sme.SemanticId,
                            IdShort = sme.IdShort,
                            ParentSme = sme.ParentSMEId,
                            Value = v.Value.ToString()
                        }
                    )
                    .Where(s =>
                        (!searchSemanticId.IsNullOrEmpty() && s.SemanticId == searchSemanticId) ||
                        (!searchIdShort.IsNullOrEmpty() && s.IdShort == searchIdShort)
                    )
                    .Join(db.SMSets,
                        v => v.SMId,
                        s => s.Id,
                        (v, s) => new
                        {
                            Id = s.Id,
                            SemanticId = s.SemanticId,
                            ParentSme = v.ParentSme,
                            Value = v.Value
                        }
                    )
                    .Where(s =>
                        smSemanticId == "" || s.SemanticId == smSemanticId
                    )
                    .ToList());

                list.AddRange(db.DValueSets.Where(v =>
                    (withEqual && withF && v.Value == fEqual)
                    )
                    .Join(db.SMESets,
                        v => v.SMEId,
                        sme => sme.Id,
                        (v, sme) => new
                        {
                            SMId = sme.SMId,
                            SemanticId = sme.SemanticId,
                            IdShort = sme.IdShort,
                            ParentSme = sme.ParentSMEId,
                            Value = v.Value.ToString()
                        }
                    )
                    .Where(s =>
                        (!searchSemanticId.IsNullOrEmpty() && s.SemanticId == searchSemanticId) ||
                        (!searchIdShort.IsNullOrEmpty() && s.IdShort == searchIdShort)
                    )
                    .Join(db.SMSets,
                        v => v.SMId,
                        s => s.Id,
                        (v, s) => new
                        {
                            Id = s.Id,
                            SemanticId = s.SemanticId,
                            ParentSme = v.ParentSme,
                            Value = v.Value
                        }
                    )
                    .Where(s =>
                        smSemanticId == "" || s.SemanticId == smSemanticId
                    )
                    .ToList());

                Console.WriteLine("Found " + list.Count() + " SMEs in " + watch.ElapsedMilliseconds + "ms");

                var hSubmodel = new HashSet<long>();
                var lParentParentNum = new List<int?>();
                foreach (var l in list)
                {
                    hSubmodel.Add(l.Id);
                    var smeDB = db.SMESets.Where(s => s.Id == l.ParentSme).First();
                    lParentParentNum.Add(smeDB.ParentSMEId);
                }

                Console.WriteLine("Found " + hSubmodel.Count() + " Submodels");

                watch.Restart();

                var SMEResult = db.SMESets.Where(s =>
                    hSubmodel.Contains(s.SMId) &&
                    ((!resultSemanticId.IsNullOrEmpty() && s.SemanticId == resultSemanticId) ||
                    (!resultIdShort.IsNullOrEmpty() && s.IdShort == resultIdShort))
                    )
                    .ToList();

                if (equal.IsNullOrEmpty())
                    equal = contains;

                foreach (var l in SMEResult)
                {
                    SMEResult r = new SMEResult();
                    bool found = false;

                    var submodelDB = db.SMSets.Where(s => s.Id == l.SMId).First();
                    if (submodelDB != null && (smSemanticId.IsNullOrEmpty() || submodelDB.SemanticId == smSemanticId))
                    {
                        r.value = equal;
                        r.url = "";
                        r.smId = submodelDB.Identifier;
                        string path = l.IdShort;
                        int? pId = l.ParentSMEId;
                        while (pId != null)
                        {
                            var smeDB = db.SMESets.Where(s => s.Id == pId).First();
                            path = smeDB.IdShort + "." + path;
                            pId = smeDB.ParentSMEId;
                            if (lParentParentNum.Contains(pId))
                            {
                                found = true;
                                if (l.SMEType == "F")
                                {
                                    var v = db.SValueSets.Where(v => v.SMEId == l.Id).FirstOrDefault();
                                    if (v.Value.ToLower().StartsWith("http"))
                                        r.url = v.Value;
                                }
                            }
                        }
                        r.idShortPath = path;
                        string sub64 = Base64UrlEncoder.Encode(r.smId);
                        if (r.url.IsNullOrEmpty())
                            r.url = ExternalBlazor + "/submodels/" + sub64 + "/submodel-elements/" + path + "/attachment";
                        if (found)
                            result.Add(r);
                    }
                }
                Console.WriteLine("Collected result in " + watch.ElapsedMilliseconds + "ms");
            }

            return result;
        }

        // --------------- SM Methodes ---------------
        private List<SMSet> getSMSet(string semanticId = "")
        {
            if (semanticId.IsNullOrEmpty())
                return new List<SMSet>();
            return new AasContext().SMSets.Where(s => s.SemanticId != null && s.SemanticId.Equals(semanticId)).ToList();
        }

        private List<SMResult> getSMResult(List<SMSet> smList)
        {
            return smList.ConvertAll(
                sm =>
                {
                    string identifier = (sm != null && sm.Identifier != null) ? sm.Identifier : "";
                    return new SMResult()
                    {
                        smId = identifier,
                        url = $"{ExternalBlazor}/submodels/{Base64UrlEncoder.Encode(identifier)}"
                    };
                }
            );
        }

        // --------------- SME Methodes ---------------
        private class smeWithValue
        {
            public SMESet? sme;
            public string? value;
            public string? smId;
        }

        private List<SMESet> getSMESet(string semanticId = "")
        {
            if (semanticId.IsNullOrEmpty())
                return new List<SMESet>();
            return new AasContext().SMESets.Where(s => s.SemanticId != null && s.SemanticId.Equals(semanticId)).ToList();
        }

        private List<SValueSet> getSValueSet(string contains = "", string equal = "")
        {
            if (contains.IsNullOrEmpty() && equal.IsNullOrEmpty())
                return new List<SValueSet>();
            return new AasContext().SValueSets.Where(v => v.Value != null && (v.Value.Contains(contains) || v.Value.Equals(equal))).ToList();
        }

        private List<IValueSet> getIValueSet(string equal = "", string lower = "", string upper = "")
        {
            try
            {
                if (!equal.IsNullOrEmpty())
                {
                    long iEqual = Convert.ToInt64(equal);
                    return new AasContext().IValueSets.Where(v => v.Value != null && v.Value == iEqual).ToList();
                }
                else if (!lower.IsNullOrEmpty() && !upper.IsNullOrEmpty())
                {
                    long iLower = Convert.ToInt64(lower);
                    long iUpper = Convert.ToInt64(upper);
                    return new AasContext().IValueSets.Where(v => v.Value != null && v.Value >= iLower && v.Value <= iUpper).ToList();
                }
            }
            catch { }
            return new List<IValueSet>();
        }

        private List<DValueSet> getDValueSet(string equal = "", string lower = "", string upper = "")
        {
            try
            {
                if (!equal.IsNullOrEmpty())
                {
                    double dEqual = Convert.ToInt64(equal);
                    return new AasContext().DValueSets.Where(v => v.Value != null && v.Value == dEqual).ToList();
                }
                else if (!lower.IsNullOrEmpty() && !upper.IsNullOrEmpty())
                {
                    double dLower = Convert.ToInt64(lower);
                    double dUpper = Convert.ToInt64(upper);
                    return new AasContext().DValueSets.Where(v => v.Value != null && v.Value >= dLower && v.Value <= dUpper).ToList();
                }
            }
            catch { }
            return new List<DValueSet>();
        }

        private List<smeWithValue> combineSMEValue(bool withSmId, List<SMSet> smList, List<SMESet> smeList, List<SValueSet> sValueList, List<IValueSet> iValueList, List<DValueSet> dValueList)
        {
            if (smeList.Count() == 0 || (sValueList.Count() == 0 && iValueList.Count() == 0 && dValueList.Count() == 0))
                return new List<smeWithValue>();

            var resultS = smeList
                .Join(sValueList,
                sme => sme.Id,
                v => v.SMEId,
                (sme, v) => new smeWithValue
                {
                    sme = sme,
                    value = v.Value
                }
            );
            var resultI = smeList
                .Join(iValueList,
                sme => sme.Id,
                v => v.SMEId,
                (sme, v) => new smeWithValue
                {
                    sme = sme,
                    value = v.Value.ToString()
                }
            );
            var resultD = smeList
                .Join(dValueList,
                sme => sme.Id,
                v => v.SMEId,
                (sme, v) => new smeWithValue
                {
                    sme = sme,
                    value = v.Value.ToString()
                }
            );
            var resultSME = resultS.Concat(resultI).Concat(resultD);

            if (!withSmId)
            {
                return resultSME
                    .Join(new AasContext().SMSets,
                    sme => sme.sme.SMId,
                    sm => sm.Id,
                    (sme, sm) => new smeWithValue
                    {
                        sme = sme.sme,
                        value = sme.value,
                        smId = sm.Identifier
                    }
                ).ToList();
            }
            else
            {
                return smList
                .Join(resultSME,
                    sm => sm.Id,
                    sme => sme.sme.SMId,
                    (sm, sme) => new smeWithValue
                    {
                        sme = sme.sme,
                        value = sme.value,
                        smId = sm.Identifier
                    }
                ).ToList();
            }
        }

        private List<SMEResult> getSMEResult(List<smeWithValue> smeList)
        {
            List<SMEResult> result = new List<SMEResult>();
            using (AasContext db = new AasContext())
            {
                result = smeList.ConvertAll(
                    sme =>
                    {
                        string identifier = (sme != null && sme.smId != null) ? sme.smId : "";
                        string path = sme.sme.IdShort;
                        int? pId = sme.sme.ParentSMEId;
                        while (pId != null)
                        {
                            var smeDB = db.SMESets.Where(s => s.Id == pId).First();
                            path = $"{smeDB.IdShort}.{path}";
                            pId = smeDB.ParentSMEId;
                        }
                        return new SMEResult()
                        {
                            smId = identifier,
                            value = sme.value,
                            idShortPath = path,
                            url = $"{ExternalBlazor}/submodels/{Base64UrlEncoder.Encode(identifier)}/submodel-elements/{path}"
                        };
                    }
                );
            }
            return result;
        }
    }
}
