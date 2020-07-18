using TenBot.AddonReader.Readers.Units;

namespace TenBot.Core.WowPlayer
{
    public class PlayerTarget
    {
        public PlayerTarget(TargetReader targetReader, TargetOfTarget targetOfTarget)
        {
            Reader = targetReader;
            Target = targetOfTarget;
        }

        public TargetReader Reader { get; }
        public TargetOfTarget Target { get; }
    }
}