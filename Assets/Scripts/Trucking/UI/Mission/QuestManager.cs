using System.Collections.Generic;
using System.Linq;
using DatasTypes;
using Trucking.Common;

namespace Trucking.UI.Mission
{
    public class QuestManager : Singleton<QuestManager>
    {
        public static List<QuestData> list = Datas.questData.ToArray().ToList();
        
        public static QuestData GetQuest(QuestData.eType qid)
        {
            QuestData questData = list.Find(x => x.id == qid.ToString());
            return questData;
        }
        
        public static FactorTypeData.eType GetFactorType(QuestData.eType qid)
        {                        
            QuestData questData = GetQuest(qid);

            if (questData != null)
            {
                return questData.factor_type.type;
            }
            
            return FactorTypeData.eType.none;
        }
        
        public static int GetCountMagType(QuestData.eType qid)
        {                        
            QuestData questData = GetQuest(qid);

            if (questData != null)
            {
                return questData.count_mag_type;
            }
            
            return 1;
        }       
        
    }
}