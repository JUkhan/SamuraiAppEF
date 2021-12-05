using System;
namespace SamuraiApp.Data
{
    public class BuusinessDataLogic
    {
        private readonly SamuraiContext context;

        public BuusinessDataLogic(SamuraiContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int AddSamuraiNames(params string[] names)
        {
            foreach(var name in names)
            {
                context.Samuraies.Add(new Domain.Samurai { Name = name });
            }
            return context.SaveChanges();
        }
    }
}
