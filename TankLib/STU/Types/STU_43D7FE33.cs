// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x43D7FE33)]
    public class STU_43D7FE33 : STUInstance {
        [STUFieldAttribute(0xB48F1D22, "m_name")]
        public teStructuredDataAssetRef<ulong> m_name;

        [STUFieldAttribute(0x29E8F0CC, "m_mapCatalog")]
        public teStructuredDataAssetRef<STUMapCatalog> m_mapCatalog;

        [STUFieldAttribute(0x7F965AB2, ReaderType = typeof(InlineInstanceFieldReader))]
        public STU_7459F2CE[] m_7F965AB2;
    }
}
