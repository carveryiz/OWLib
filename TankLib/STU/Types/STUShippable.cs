// Instance generated by TankLibHelper.InstanceBuilder
using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x8FFAF278, "STUShippable")]
    public class STUShippable : STUInstance {
        [STUFieldAttribute(0x38BFB46C, "m_resourceKey")]
        public teStructuredDataAssetRef<STUResourceKey> m_resourceKey;

        [STUFieldAttribute(0xBB16810A, "m_priority")]
        public Enum_D407CA8B m_priority;

        [STUFieldAttribute(0xB7A1D145, "m_chunkId")]
        public int m_chunkId;

        [STUFieldAttribute(0x6461415F)]
        public byte m_6461415F;

        [STUFieldAttribute(0x1C50268A)]
        public byte m_1C50268A;

        [STUFieldAttribute(0x646B9249, "m_complete")]
        public byte m_complete;
    }
}
