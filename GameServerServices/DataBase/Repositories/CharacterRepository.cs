
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameServerServices.DataBase.Contexts;
using GameServerServices.DataBase.Models;

namespace GameServerServices.DataBase.Repositories
{
    public class CharacterRepository:IRepository<Models.Character>
    {

        public CharacterRepository(IDataContext dataContext)
        {
            CharacterSet = dataContext.Set<Character>();
        }
        protected DbSet<Character> CharacterSet { get; private set; }
        public IQueryable<Character> Get() => CharacterSet;

        public void Add(Character character)
        {
            if (character == null)
            {
                throw new ArgumentException();
            }

            CharacterSet.Attach(character);
            CharacterSet.Add(character);
        }

        public void Update(int characterId, Character newCharacter)
        {
            var characterToChange = CharacterSet.Find(characterId);

            if (characterToChange != null)
            {
                CharacterSet.Attach(characterToChange);
                characterToChange.Name = newCharacter.Name;
                characterToChange.MainRedColor = newCharacter.MainRedColor;
                characterToChange.MainGreenColor = newCharacter.MainGreenColor;
                characterToChange.MainBlueColor = newCharacter.MainBlueColor;
                characterToChange.StrokeRedColor = newCharacter.StrokeRedColor;
                characterToChange.StrokeGreenColor = newCharacter.StrokeGreenColor;
                characterToChange.StrokeBlueColor = newCharacter.StrokeBlueColor;
                characterToChange.StrokeLength = newCharacter.StrokeLength;
                characterToChange.StrokeSpace = newCharacter.StrokeSpace;
            }
        }

        public void Remove(int characterId)
        {
            var characterToRemove =  CharacterSet.Find(characterId);

            if (characterToRemove != null)
            {
                CharacterSet.Attach(characterToRemove);
                CharacterSet.Remove(characterToRemove);
            }
        }

        public async Task RemoveAsync(int characterId)
        {
            var characterToRemove = await CharacterSet.FindAsync(characterId);

            if (characterToRemove != null)
            {
                CharacterSet.Attach(characterToRemove);
                CharacterSet.Remove(characterToRemove);
            }
        }


        public async Task UpdateAsync(int characterId, Character newCharacter)
        {
            var characterToChange = await CharacterSet.FindAsync(characterId);

            if (characterToChange != null)
            {
                CharacterSet.Attach(characterToChange);
                characterToChange.Name = newCharacter.Name;
                characterToChange.MainRedColor = newCharacter.MainRedColor;
                characterToChange.MainGreenColor = newCharacter.MainGreenColor;
                characterToChange.MainBlueColor = newCharacter.MainBlueColor;
                characterToChange.StrokeRedColor = newCharacter.StrokeRedColor;
                characterToChange.StrokeGreenColor = newCharacter.StrokeGreenColor;
                characterToChange.StrokeBlueColor = newCharacter.StrokeBlueColor;
                characterToChange.StrokeLength = newCharacter.StrokeLength;
                characterToChange.StrokeSpace = newCharacter.StrokeSpace;
            }
        }

    }
}