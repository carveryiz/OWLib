﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_43515 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx = header.BuildVersion - kidx;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= 0x2B;
                buffer[i] ^= digest[SignedMod(kidx + header.DataCount, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0xEE, 0xF8, 0x0D, 0x93, 0x26, 0x31, 0xE9, 0x0D, 0xFF, 0x07, 0xE7, 0xFA, 0xE8, 0xFD, 0x3E, 0x05,
            0xA0, 0xA7, 0x49, 0xC7, 0x5C, 0xCC, 0x24, 0x78, 0x4D, 0x3C, 0xDD, 0x4B, 0x6A, 0xBF, 0xDF, 0x79,
            0xA9, 0x73, 0xA4, 0x7B, 0x9B, 0x50, 0x6C, 0xFF, 0xA1, 0x2C, 0xFA, 0x6C, 0x96, 0x79, 0x8F, 0x31,
            0x6C, 0x97, 0x74, 0x94, 0x20, 0x47, 0x70, 0x09, 0x37, 0x28, 0x62, 0x46, 0x4A, 0xA3, 0x83, 0xD3,
            0xD4, 0xE4, 0x59, 0xAA, 0x85, 0x13, 0x67, 0x7E, 0xD6, 0x59, 0x0A, 0x84, 0x06, 0x7C, 0x19, 0xB1,
            0x8A, 0xE6, 0xEC, 0x2A, 0xAE, 0x01, 0xBB, 0x23, 0x15, 0x50, 0x2C, 0x0D, 0xF0, 0x25, 0x68, 0x1C,
            0x15, 0xCC, 0x06, 0xB5, 0x88, 0xEF, 0x55, 0xF4, 0xD0, 0xE9, 0x6D, 0x16, 0x58, 0x68, 0x0F, 0xD4,
            0xDF, 0x53, 0xE7, 0xC3, 0x9D, 0x13, 0xEE, 0x64, 0x56, 0xE8, 0x58, 0xAB, 0x10, 0xFA, 0xDC, 0x2A,
            0xC4, 0x9B, 0xE4, 0x5F, 0xDD, 0x9F, 0x28, 0xCA, 0x82, 0x9B, 0xC8, 0x4E, 0x37, 0x19, 0x1F, 0x34,
            0x13, 0x06, 0x00, 0xE1, 0x31, 0xA7, 0xD4, 0x7D, 0x6E, 0xBE, 0x95, 0xD6, 0x45, 0xC9, 0x05, 0x0B,
            0xB8, 0xBD, 0x07, 0x98, 0x5C, 0x48, 0x46, 0x26, 0xCF, 0x1B, 0x9E, 0x2C, 0x96, 0xB3, 0x5F, 0x9E,
            0x36, 0x07, 0xA5, 0x61, 0x87, 0x49, 0x4D, 0x0A, 0x79, 0xA8, 0x8F, 0x0F, 0x9D, 0x5E, 0xEB, 0x07,
            0x0D, 0x7E, 0xF8, 0x1B, 0xC8, 0x9F, 0x7B, 0x2A, 0x1D, 0x69, 0x24, 0x81, 0x4E, 0x09, 0xC7, 0xD1,
            0x54, 0x00, 0x20, 0x2E, 0xE8, 0x60, 0x4C, 0x71, 0x04, 0x96, 0x03, 0x4E, 0xA5, 0x9B, 0x41, 0xB5,
            0x02, 0x91, 0x0B, 0x3F, 0x84, 0x77, 0x2F, 0xCD, 0x19, 0x00, 0xDB, 0x23, 0xB7, 0xBC, 0x47, 0xBD,
            0x62, 0x36, 0x54, 0xCD, 0x99, 0x87, 0x0F, 0x63, 0xD9, 0x74, 0xD1, 0xD1, 0xE2, 0x5E, 0x2B, 0xC6,
            0xDA, 0x96, 0x57, 0x28, 0x9C, 0x54, 0xF4, 0x2C, 0xB9, 0xE1, 0x2F, 0x15, 0xCF, 0x04, 0x2E, 0xE1,
            0xFD, 0x44, 0x08, 0xC3, 0xED, 0xE1, 0x58, 0xD4, 0x40, 0x6F, 0x26, 0x11, 0x64, 0xC8, 0x5D, 0x13,
            0x63, 0xAE, 0xCD, 0x4C, 0xC0, 0x1D, 0x73, 0xF2, 0xF9, 0x4D, 0x2D, 0x1C, 0x9E, 0xFB, 0xF5, 0xEF,
            0x8D, 0x78, 0x51, 0xB3, 0x54, 0x23, 0xC5, 0x66, 0x48, 0xF6, 0xCA, 0xD0, 0xF0, 0xFE, 0x18, 0x97,
            0x88, 0x78, 0x55, 0x22, 0xA5, 0x7D, 0x33, 0xCA, 0xBD, 0x91, 0x21, 0x2D, 0xEC, 0x10, 0x42, 0xEF,
            0x5C, 0xC0, 0x5E, 0x34, 0x71, 0x0A, 0x9A, 0x14, 0xBA, 0x9B, 0xA4, 0xAE, 0x3D, 0xA2, 0xC4, 0x5F,
            0x02, 0xD2, 0x67, 0x8D, 0xC7, 0x2C, 0xFE, 0xAE, 0x34, 0xD7, 0xA7, 0x36, 0x3B, 0x67, 0x35, 0x36,
            0x3A, 0xE4, 0xDA, 0x40, 0x83, 0x60, 0xDF, 0x24, 0x0E, 0x36, 0x43, 0x29, 0xB8, 0xDA, 0x22, 0xBA,
            0xA7, 0x73, 0x7F, 0xE3, 0x9A, 0xFE, 0xBE, 0x7C, 0xF9, 0xA0, 0xF7, 0x92, 0xCB, 0xDA, 0x03, 0xA7,
            0x76, 0x82, 0xA2, 0x9C, 0x39, 0x40, 0x86, 0xD6, 0x02, 0x4E, 0x8B, 0xDB, 0x11, 0xBE, 0x82, 0x81,
            0x27, 0x97, 0x53, 0x32, 0x63, 0x14, 0x47, 0xC8, 0x7D, 0x3E, 0x21, 0x04, 0xBB, 0x4A, 0x26, 0x24,
            0xF5, 0x84, 0x13, 0x7C, 0xBD, 0x6D, 0x1F, 0xB4, 0x91, 0xF1, 0x2E, 0x72, 0x1C, 0xCA, 0xCC, 0xA1,
            0x22, 0x36, 0x5F, 0x66, 0xC0, 0x16, 0xFB, 0x94, 0x0E, 0xD7, 0x82, 0x0D, 0x95, 0xEF, 0x94, 0x12,
            0x97, 0xC0, 0xBE, 0xBE, 0x75, 0x79, 0x9B, 0x35, 0x22, 0x77, 0xBA, 0x85, 0x93, 0xF0, 0x2F, 0x2E,
            0x3E, 0xFC, 0x18, 0x25, 0x0F, 0xC3, 0xE5, 0x9E, 0x8B, 0xA9, 0x6D, 0xDB, 0xA1, 0x7E, 0x7B, 0x2E,
            0x37, 0x54, 0xFA, 0x11, 0xEC, 0x7B, 0x83, 0x0E, 0x1F, 0x28, 0xED, 0x1A, 0x75, 0x1C, 0xB9, 0x6A
        };
    }
}