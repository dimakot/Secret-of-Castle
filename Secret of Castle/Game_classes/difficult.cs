using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Secret_of_Castle
{
    internal class difficult
    {
        public string CurrentDifficulty;
        private static difficult instance = null;
        private difficult() { }
        public static difficult Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new difficult();
                }
                return instance;
            }
        }
        public void SetDifficulty(string difficulty)
        {
            CurrentDifficulty = difficulty;
        }
    }
}
