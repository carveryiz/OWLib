﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_37130 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = 130;
            const uint increment = 266;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx -= increment;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.DataCount & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                switch (kidx % 3) {
                    case 0:
                        kidx += 103;
                        break;
                    case 1:
                        kidx = 4 * kidx % header.BuildVersion;
                        break;
                    case 2:
                        --kidx;
                        break;
                }

                buffer[i] ^= digest[(kidx + header.BuildVersion) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }
        
        private static readonly byte[] Keytable = {
            0x00, 0x80, 0x1F, 0xAB, 0xAC, 0xFC, 0xFA, 0x6F, 0xE6, 0x91, 0xE7, 0x1A, 0xA8, 0xAA, 0x23, 0xE3, 
            0x7C, 0x67, 0x59, 0x20, 0xDF, 0x2B, 0xB0, 0x1D, 0xF2, 0x9A, 0x6E, 0x8B, 0x53, 0x31, 0x0F, 0x44, 
            0x9D, 0xA7, 0xB0, 0x1B, 0xBC, 0xA5, 0xEE, 0x32, 0x2B, 0x13, 0x27, 0x87, 0x4B, 0x09, 0xEA, 0x03, 
            0x46, 0x64, 0xE3, 0x96, 0x36, 0xD0, 0x02, 0x88, 0xAA, 0xB2, 0x98, 0x02, 0x42, 0xFA, 0x07, 0xEE, 
            0x2C, 0xEA, 0xBE, 0x8C, 0x1F, 0x6B, 0x41, 0x11, 0xC6, 0x93, 0x69, 0xEF, 0x98, 0xD2, 0xDD, 0x1F,
            0x5C, 0x60, 0xDF, 0x09, 0x8B, 0x69, 0x57, 0x23, 0x43, 0xA0, 0xDD, 0x50, 0x56, 0x5A, 0xB9, 0x96, 
            0x0F, 0x6E, 0x63, 0x11, 0x4F, 0x39, 0x4B, 0x93, 0xB6, 0x66, 0xCA, 0xCA, 0xC2, 0xD5, 0x6F, 0x82, 
            0x0E, 0x71, 0xD9, 0x29, 0x61, 0x04, 0x4D, 0xDC, 0x4E, 0x28, 0x9F, 0x3F, 0xDB, 0x4A, 0xC2, 0x15,
            0x6A, 0x0F, 0xEB, 0xB9, 0xC8, 0x28, 0x86, 0x7B, 0xFD, 0xBA, 0xF6, 0x4D, 0x35, 0xDC, 0x5A, 0xC8, 
            0xDC, 0x9F, 0x13, 0x20, 0x01, 0x59, 0xFE, 0x30, 0x2B, 0xDD, 0x77, 0x3D, 0xE4, 0xC9, 0x91, 0xD6, 
            0xDB, 0x04, 0x94, 0x1F, 0x02, 0x0D, 0x14, 0x62, 0x8A, 0xC0, 0x18, 0xC4, 0xF7, 0x78, 0xC9, 0x32, 
            0x5F, 0xB9, 0x7A, 0x8D, 0xB7, 0x65, 0x93, 0xC7, 0x70, 0xA0, 0x53, 0xCF, 0x51, 0x83, 0xC9, 0x79, 
            0xB1, 0x49, 0xC3, 0x47, 0xDF, 0xBA, 0xE5, 0xB6, 0xAF, 0x33, 0x89, 0xA5, 0xD6, 0x78, 0x48, 0xA6, 
            0x59, 0xD7, 0x0C, 0x40, 0xF8, 0x35, 0x58, 0x0E, 0x6C, 0xD1, 0xFB, 0x4C, 0xB4, 0xBB, 0x98, 0xB8, 
            0x07, 0x5E, 0x6F, 0x69, 0xB0, 0x74, 0xA4, 0xDB, 0xC2, 0x19, 0x69, 0x54, 0x1E, 0x1C, 0xFC, 0x60, 
            0xC7, 0xC0, 0x92, 0xA5, 0xA6, 0x81, 0x23, 0x37, 0xC1, 0xBC, 0x86, 0xF6, 0x32, 0x91, 0x53, 0x94,
            0xCC, 0x3F, 0x62, 0xA7, 0xB9, 0x4D, 0x58, 0x9F, 0xFD, 0x82, 0x82, 0x0B, 0x6C, 0xC7, 0x69, 0x2C, 
            0x5C, 0xCB, 0x27, 0x90, 0x2F, 0xD4, 0x50, 0x07, 0x4A, 0x5D, 0xB2, 0x80, 0x68, 0x8E, 0x9D, 0x34, 
            0xF6, 0xEA, 0xF6, 0x75, 0x8B, 0xF7, 0xEF, 0x24, 0xEF, 0xE7, 0x8E, 0x09, 0x9D, 0x7E, 0x93, 0x11,
            0x33, 0x97, 0xA3, 0x2D, 0xFE, 0xEB, 0xCF, 0x0B, 0xEC, 0xF3, 0x3F, 0xFB, 0x5C, 0xD9, 0xA7, 0xB1, 
            0xA6, 0xC8, 0x12, 0xAA, 0xD9, 0x9E, 0x04, 0xDE, 0xF3, 0x51, 0x12, 0xDD, 0x52, 0x25, 0xEB, 0x92, 
            0x28, 0x3C, 0xE6, 0x57, 0x20, 0x3A, 0x4D, 0xF9, 0xB2, 0x2A, 0x92, 0xB4, 0xB3, 0x55, 0xDD, 0xCA, 
            0x8A, 0xCD, 0x6A, 0xC7, 0xC8, 0x12, 0x0A, 0x8C, 0x15, 0xB4, 0x23, 0x19, 0xA5, 0x04, 0x92, 0x99, 
            0xFC, 0xB7, 0xE4, 0xDE, 0x06, 0xC2, 0x93, 0x21, 0x91, 0x07, 0x42, 0xF9, 0x55, 0x9A, 0xCA, 0x7B, 
            0x78, 0x86, 0x55, 0xE6, 0xB7, 0xA4, 0x52, 0xFB, 0x2D, 0x97, 0xFE, 0xE3, 0x62, 0x04, 0xF5, 0x9F, 
            0x41, 0xD4, 0x79, 0xF4, 0x58, 0x01, 0x94, 0xF8, 0x04, 0x85, 0x8A, 0xA6, 0x9A, 0x4D, 0xD5, 0xAB, 
            0x30, 0xED, 0x95, 0x20, 0xD6, 0x91, 0x80, 0xFC, 0xAD, 0xC9, 0x61, 0xC0, 0x91, 0xD8, 0x70, 0xF9,
            0xCF, 0x0B, 0xDA, 0x76, 0x96, 0xA3, 0x08, 0xCD, 0xBB, 0x6D, 0xBB, 0xD9, 0x58, 0x5B, 0x4A, 0x92, 
            0xCB, 0x72, 0xE2, 0x8C, 0xC1, 0x7E, 0x03, 0xC3, 0xCF, 0xA3, 0x11, 0x24, 0xF5, 0xF3, 0x4A, 0x91, 
            0xD1, 0xE1, 0x5C, 0x79, 0x6E, 0x99, 0xE5, 0x55, 0x88, 0x01, 0x61, 0x93, 0xDA, 0xCF, 0xC6, 0x78,
            0x5D, 0x6D, 0x8C, 0x31, 0xEC, 0xFB, 0x5F, 0xBA, 0xAB, 0x0B, 0x3D, 0xB3, 0xCA, 0xFE, 0xD9, 0x6A, 
            0x6C, 0xD4, 0x8B, 0xDE, 0x0B, 0xB3, 0xD9, 0x61, 0x1F, 0x3D, 0x0F, 0x94, 0x5C, 0x93, 0xD9, 0x9C
        };
    }
}