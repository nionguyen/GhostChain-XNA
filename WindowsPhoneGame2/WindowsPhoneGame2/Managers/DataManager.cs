
namespace WindowsPhoneGame2
{
    public class DataManager
    {
        public Data data;
        private static DataManager s_pInstance = null;

        #region Get and Set
        public int LevelCount
        {
            get { return data.LevelCount; }
            set { data.LevelCount = value; }
        }
        
        public int EasyHighScore
        {
            get { return data.EasyHighScore; }
            set { data.EasyHighScore = value; }
        }
        public int NormalHighScore
        {
            get { return data.NormalHighScore; }
            set { data.NormalHighScore = value; }
        }

        public int CurrentLevel
        {
            get { return data.CurrentLevel; }
            set { data.CurrentLevel = value; }
        }
        #endregion


        private DataManager()
        {
            data = new Data();
            LoadData();
        }

        public static DataManager GetInstance()
        {
            if (s_pInstance == null)
            {
                s_pInstance = new DataManager();
                return s_pInstance;
            }
            else
                return s_pInstance;
        }

        private void LoadData()
        {
            data = SaveGameXML.ReadDataFromXML();
        }
        public void SaveData()
        {
            SaveGameXML.SaveGame(data);
        }

    }
}
