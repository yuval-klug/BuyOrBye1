using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyOrBye.Models
{
    public class algoSimilarity_paired
    {
        string category_name;
        string category_paired;
        float pair_weight;
        int pair_counter;

        private static List<algoSimilarity_paired> algoSimilarity_pairedList = new List<algoSimilarity_paired>();

        public string Category_name { get => category_name; set => category_name = value; }
        public string Category_paired { get => category_paired; set => category_paired = value; }
        public float Pair_weight { get => pair_weight; set => pair_weight = value; }
        public int Pair_counter { get => pair_counter; set => pair_counter = value; }

        public algoSimilarity_paired(string category_name, string category_paired, float pair_weight, int pair_counter)
        {
            this.category_name = category_name;
            this.category_paired = category_paired;
            this.pair_weight = pair_weight;
            this.pair_counter = pair_counter;
        }

        public algoSimilarity_paired() { }

        public List<algoSimilarity_paired> getalgoSimilarity_pairedList()
        {
            DBservices db2 = new DBservices();
            List<algoSimilarity_paired> listalgoSimilarity_pairedArr = db2.getalgoSimilarity_pairedListDAL();
            return listalgoSimilarity_pairedArr;
        }

        public int InsertalgoSimilarity_paired()
        {
            DBservices dbsLike = new DBservices();
            int numAffected = dbsLike.algoSimilarity_pairedDAL(this);
            return numAffected;
        }

    }
}