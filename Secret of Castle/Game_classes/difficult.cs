namespace Secret_of_Castle
{
    internal class difficult
    {
        public string CurrentDifficulty = "Lite"; //Текущая сложность
        private static difficult instance = null; //Создаем экземпляр класса
        private difficult() { } //Конструктор
        public static difficult Instance //Создаем экземпляр класса
        {
            get
            {
                if (instance == null) //Если экземпляр класса не создан, то создаем его
                {
                    instance = new difficult(); ///Создаем экземпляр класса
                }
                return instance; 
            }
        }
        public void SetDifficulty(string difficulty) //Устанавливаем сложность
        {
            CurrentDifficulty = difficulty; //Устанавливаем сложность
        }
    }
}
