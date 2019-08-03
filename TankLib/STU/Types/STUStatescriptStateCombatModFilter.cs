// Instance generated by TankLibHelper.InstanceBuilder
using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x6226036A, "STUStatescriptStateCombatModFilter")]
    public class STUStatescriptStateCombatModFilter : STU_FC063208 {
        [STUFieldAttribute(0xF60AA143)]
        public teStructuredDataAssetRef<STUIdentifier>[] m_F60AA143;

        [STUFieldAttribute(0x5CFFCBAE)]
        public teStructuredDataAssetRef<STUIdentifier>[] m_5CFFCBAE;

        [STUFieldAttribute(0x60703B1E)]
        public teStructuredDataAssetRef<ulong> m_60703B1E;

        [STUFieldAttribute(0x50530EAB, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_50530EAB;

        [STUFieldAttribute(0x6AA5D70F, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVarFilter m_6AA5D70F;

        [STUFieldAttribute(0x941C56C2, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_941C56C2;

        [STUFieldAttribute(0x893AABFC, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_893AABFC;

        [STUFieldAttribute(0xF7872851, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_F7872851;

        [STUFieldAttribute(0xE9517777, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_E9517777;

        [STUFieldAttribute(0x41B10FF2)]
        public Enum_32773B82 m_41B10FF2;
    }
}
