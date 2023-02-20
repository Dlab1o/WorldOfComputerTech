using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WorldOfComputerTech
{
    class TopDeck
    {
        public static string CurrentUserName = String.Empty;
        public static int CurrentUserRoleID = 0;
        public static string CurrentUserRoleName = String.Empty;
        public static string CurrentEmpPhotoName = String.Empty;

        public static int EmpToEditID = 0;

        public static int ClientToCreative = 0;




        public static bool CheckAllSpace(string Text)
        {
            
            if (Text == null || Text.Length == 0) return true;
            for (int i = 0; i < Text.Length; i++) if (!char.IsWhiteSpace(Text[i])) return false;
            return true;
            
        }
    }
}
