namespace ExamPrep.Data
{
    public static class Utility
    {
        public static void InitDB()
        {
            var context = new MassDefectEntities();
            context.Database.Initialize(true);
        }
    }
}
