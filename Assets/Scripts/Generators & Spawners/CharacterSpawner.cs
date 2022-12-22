using ColorManRun.Characters;
using ColorManRun.Factories;
using UnityEngine;

namespace ColorManRun.Generators
{
    public class CharacterSpawner : MonoBehaviour
    {
        public Character Spawn(Character characterPrefab, StatsConfig config, Vector3 position)
        {
            var character = Instantiate(characterPrefab, position, Quaternion.identity);

            var factory = new CharacterFactory();
            character = factory.InitStats(character, config);
            return character;
        }
        public T Spawn<T>(T characterPrefab, Vector3 position) 
            where T : CharacterAwakeInit =>
            Instantiate(characterPrefab, position, Quaternion.identity);

        public Character SpawnAgent(Character characterPrefab, StatsConfig config, AgentTarget target, Vector3 position)
        {
            var character = Instantiate(characterPrefab, position, Quaternion.identity);

            var factory = new AgentCharacterFactory(target);
            character = factory.InitStats(character, config);
            character = factory.SetupMovement(character);
            return character;
        }

        public T SpawnAgent<T>(T characterPrefab, AgentTarget target, Vector3 position) 
            where T : CharacterAwakeInit
        {
            var character = Instantiate(characterPrefab, position, Quaternion.identity);

            var factory = new AgentCharacterFactory(target);
            character = factory.SetupMovement(character);
            return character;
        }
    }
}
