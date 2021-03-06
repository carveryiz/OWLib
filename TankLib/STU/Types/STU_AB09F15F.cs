// Instance generated by TankLibHelper.InstanceBuilder
using TankLib.Math;
using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0xAB09F15F)]
    public class STU_AB09F15F : STUInstance {
        [STUFieldAttribute(0x11B47C68, "m_id")]
        public teUUID m_id;

        [STUFieldAttribute(0x4D2DB658, "m_identifier")]
        public teStructuredDataAssetRef<STUIdentifier> m_identifier;

        [STUFieldAttribute(0x07DD813E, "m_value")]
        public teVec4 m_value;

        [STUFieldAttribute(0x0619C597, "m_type")]
        public Enum_AE1B6533 m_type;
    }
}
