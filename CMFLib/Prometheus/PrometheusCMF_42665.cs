﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_42665 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Keytable[header.BuildVersion & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += 3;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = (uint) length * header.BuildVersion;
            uint increment = kidx % 13;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += increment;
                buffer[i] ^= digest[SignedMod(kidx + 0x15666D63, SHA1_DIGESTSIZE)];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x0C, 0x07, 0x07, 0x84, 0x02, 0x01, 0x35, 0x92, 0x11, 0xDC, 0x4B, 0xF1, 0x43, 0x73, 0xFC, 0x72,
            0xF8, 0x57, 0x64, 0xC0, 0x18, 0x0A, 0x8D, 0x59, 0xA2, 0x8C, 0xE0, 0xB3, 0xB7, 0x4A, 0xD7, 0x77,
            0xE4, 0x70, 0x45, 0x77, 0x4D, 0x69, 0x22, 0xCA, 0x68, 0xED, 0x19, 0xD0, 0x58, 0xA8, 0x9B, 0xFC,
            0xB2, 0xD8, 0xE6, 0x02, 0x97, 0x53, 0x6D, 0xEB, 0x77, 0x47, 0x9D, 0x85, 0xAF, 0x30, 0xC2, 0x14,
            0xC1, 0xBF, 0xFA, 0x91, 0xA6, 0x8B, 0x0C, 0xAA, 0x78, 0x11, 0xF0, 0x5A, 0xCB, 0xD2, 0xA8, 0x70,
            0x26, 0xB8, 0x8B, 0x88, 0x10, 0x14, 0x28, 0xA9, 0x51, 0x5A, 0x40, 0x5B, 0x4F, 0x3B, 0x10, 0xF7,
            0x13, 0x23, 0xCA, 0x8F, 0x2E, 0xE1, 0x96, 0xC1, 0xC1, 0x60, 0xF0, 0xA5, 0xBA, 0x20, 0xD1, 0xE6,
            0xC4, 0x80, 0x9C, 0x50, 0x5D, 0x0B, 0xB4, 0xCA, 0x04, 0xCA, 0x9E, 0x7A, 0x01, 0xE9, 0x84, 0x62,
            0x68, 0x67, 0xD0, 0xC2, 0x63, 0x36, 0x22, 0x2A, 0x48, 0x61, 0x1B, 0x93, 0x51, 0x37, 0x90, 0xB3,
            0xE8, 0xAF, 0x2E, 0x4C, 0x92, 0x62, 0xF8, 0x42, 0x63, 0x26, 0xD5, 0xB5, 0x4C, 0xA8, 0x91, 0x7A,
            0xA5, 0x45, 0x16, 0xE1, 0xB3, 0x41, 0xD9, 0x43, 0xF0, 0xF1, 0x59, 0xF5, 0x32, 0x5C, 0x49, 0x4B,
            0xDA, 0x7E, 0xB6, 0x2C, 0x24, 0x27, 0xDF, 0x5E, 0x33, 0xD2, 0x96, 0x42, 0x6B, 0x23, 0x85, 0xD7,
            0x79, 0x31, 0x93, 0x88, 0x1B, 0x07, 0x69, 0x76, 0x83, 0x62, 0xE8, 0xF7, 0x29, 0x96, 0xD9, 0x6E,
            0x87, 0x39, 0x2E, 0x1B, 0x95, 0xAC, 0x4F, 0x5D, 0xC1, 0x94, 0x7E, 0xE9, 0xCE, 0xA9, 0xE2, 0x1E,
            0xD4, 0x79, 0xC8, 0x16, 0x65, 0x84, 0xDB, 0xF3, 0xDA, 0x51, 0x88, 0x16, 0x02, 0xC3, 0xE6, 0x23,
            0x8D, 0x18, 0xC5, 0xAB, 0x31, 0xDB, 0xBA, 0xD1, 0xBD, 0x24, 0x97, 0xE2, 0xCD, 0x25, 0x41, 0x7C,
            0x1B, 0x8E, 0x4B, 0x37, 0xD2, 0x6C, 0x7A, 0x2B, 0x24, 0x8A, 0x7F, 0x2E, 0x90, 0xCA, 0x10, 0x6E,
            0x78, 0x23, 0xF2, 0xD2, 0xA9, 0xB9, 0x84, 0xDA, 0x5D, 0xFE, 0xAE, 0x77, 0x2A, 0xFF, 0xB7, 0xF2,
            0x6C, 0x02, 0x77, 0x4E, 0xC2, 0xF4, 0x55, 0xD4, 0x50, 0xD9, 0x20, 0x78, 0xFC, 0x2A, 0x34, 0x38,
            0x46, 0x1D, 0xA3, 0xE4, 0x4E, 0xFA, 0x3B, 0x93, 0xFF, 0x20, 0x9C, 0x6A, 0x15, 0x9D, 0xDD, 0x8C,
            0x98, 0x1B, 0xF6, 0xAD, 0xAF, 0xBD, 0x58, 0x29, 0xDF, 0xE2, 0xBF, 0xF9, 0xB9, 0xED, 0xF0, 0xA2,
            0x98, 0xF2, 0xC9, 0x12, 0x47, 0xCC, 0x26, 0xDF, 0x9A, 0xE0, 0x67, 0x4C, 0xC4, 0x10, 0x53, 0x42,
            0xF2, 0x2D, 0x1C, 0x0B, 0xDC, 0xE3, 0xE3, 0xEB, 0xCB, 0x1B, 0xB3, 0xCF, 0x53, 0x5C, 0xD9, 0x1D,
            0xAA, 0x18, 0xA3, 0xD8, 0x42, 0xC3, 0x26, 0xE9, 0x74, 0x51, 0xB4, 0xB4, 0x94, 0x79, 0xA1, 0x4E,
            0xEF, 0x1B, 0xE9, 0x8F, 0xDE, 0x62, 0x9E, 0xB3, 0xBE, 0x98, 0xEA, 0x88, 0xEC, 0xD2, 0x0E, 0xC3,
            0x37, 0xB4, 0xFF, 0xA2, 0xA9, 0x97, 0xA9, 0xEF, 0x2B, 0x5A, 0x49, 0xA7, 0x52, 0xB5, 0x62, 0xE3,
            0x38, 0xD5, 0x16, 0xE4, 0x55, 0x68, 0xF9, 0x72, 0xD0, 0xBF, 0x3C, 0x62, 0xFD, 0x8F, 0xE3, 0xF4,
            0xB0, 0xF7, 0x7C, 0x94, 0xBD, 0x71, 0x39, 0x2E, 0xA8, 0x7C, 0xA1, 0x99, 0x55, 0x2E, 0x82, 0x03,
            0x9E, 0x69, 0x7E, 0x41, 0x74, 0xE6, 0x69, 0xCD, 0x68, 0xCE, 0x43, 0x30, 0x53, 0xDE, 0x26, 0xC9,
            0xD5, 0xD5, 0x51, 0x6F, 0xA0, 0xCC, 0x5B, 0x81, 0xC1, 0x28, 0x16, 0x93, 0x38, 0x54, 0x97, 0xB7,
            0x60, 0xD8, 0x38, 0x00, 0x95, 0x5C, 0x5C, 0x00, 0x15, 0x94, 0x2A, 0xB6, 0x89, 0xC2, 0xF0, 0x25,
            0x08, 0x89, 0x3A, 0x84, 0x1D, 0x9D, 0x25, 0xE4, 0x53, 0x94, 0xEB, 0x85, 0x5E, 0xE7, 0xC3, 0x81
        };
    }
}