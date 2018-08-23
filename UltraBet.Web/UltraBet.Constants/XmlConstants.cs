using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraBet.Constants
{
    public class XmlConstants
    {
        public const string XML_FILE_NAME = "my.xml";
        public const string XML_URL = "https://sports.ultraplay.net/sportsxml?clientKey=1bf3f918-fa27-4400-8815-d14a130f6851&sportId=2357";

        public const string XML_ELEMENT_NAME = "XmlSports";
        public const string XML_CREATE_DATE_VALUE = "CreateDate";


        public const string SPORT_ATTR = "Sport";
        public const string SPORT_NAME = "Name";
        public const string SPORT_ID = "ID";

        public const string EVENT_ATTR = "Event";
        public const string EVENT_NAME = "Name";
        public const string EVENT_ID = "ID";
        public const string EVENT_ISLIVE = "IsLive";
        public const string EVENT_CATEGORY_ID = "CategoryID";

        public const string MATCH_ATTR = "Match";
        public const string MATCH_NAME = "Name";
        public const string MATCH_ID = "ID";
        public const string MATCH_START_DATE = "StartDate";
        public const string MATCH_TYPE = "MatchType";

        public const string BET_ATTR = "Bet";
        public const string BET_NAME = "Name";
        public const string BET_ID = "ID";
        public const string BET_ISLIVE = "IsLive";

        public const string ODD_ATTR = "Odd";
        public const string ODD_NAME = "Name";
        public const string ODD_ID = "ID";
        public const string ODD_VALUE = "Value";
        public const string ODD_SPECIAL_BET_VALUE = "SpecialBetValue";
    }
}
