﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_43435 : ICMFProvider {
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
            0x63, 0x5D, 0x2B, 0x62, 0x33, 0xEE, 0xFA, 0xD3, 0x3B, 0x13, 0xD3, 0x24, 0xA5, 0xC4, 0x8B, 0xBC,
            0x0F, 0x78, 0x8D, 0x34, 0xC9, 0x40, 0xD2, 0x9A, 0x81, 0x2A, 0x55, 0xFB, 0xB7, 0x0D, 0xA1, 0xE8,
            0xC8, 0xE1, 0xFE, 0x03, 0xA7, 0x7A, 0xA5, 0xBE, 0x0B, 0xB5, 0x77, 0xE8, 0xC5, 0x75, 0x5E, 0x0C,
            0x6D, 0x0E, 0x4C, 0x30, 0x69, 0x5B, 0x08, 0x99, 0xD0, 0x03, 0x84, 0x8F, 0x4B, 0xEF, 0x3E, 0x32,
            0x80, 0xC1, 0xFD, 0xB6, 0x2D, 0x59, 0x6A, 0xDB, 0x31, 0xAD, 0x86, 0x76, 0x28, 0xB7, 0x5C, 0x30,
            0xA7, 0x35, 0xEE, 0x71, 0x90, 0x55, 0xFF, 0xD2, 0x58, 0x4B, 0x2C, 0x5D, 0x1E, 0x63, 0x23, 0xE7,
            0xAE, 0x3F, 0x0C, 0xB3, 0xBC, 0xF6, 0xFB, 0x89, 0x9F, 0x3A, 0x9F, 0x01, 0xFD, 0x83, 0x61, 0x68,
            0x66, 0x2F, 0xCB, 0x56, 0x7B, 0xA1, 0xF4, 0x3E, 0xBA, 0xB7, 0xD8, 0x37, 0x3F, 0x9F, 0x6D, 0x30,
            0x4B, 0xDF, 0xF3, 0xD1, 0xF0, 0xC6, 0x30, 0xBE, 0x52, 0xA4, 0x6D, 0x30, 0x1A, 0x51, 0xBA, 0xDF,
            0x50, 0xB9, 0xF5, 0x8B, 0xCB, 0x09, 0xB9, 0xDB, 0xA0, 0xE1, 0xE6, 0x40, 0xD9, 0xB5, 0xCF, 0xE6,
            0xBB, 0x60, 0x0C, 0x6C, 0xA0, 0x73, 0xD2, 0x41, 0x76, 0xD2, 0x80, 0x5D, 0xAF, 0x05, 0x0E, 0xD0,
            0x76, 0x8C, 0x46, 0x35, 0x93, 0x87, 0x5A, 0xCF, 0x18, 0xE7, 0x0D, 0x5A, 0x12, 0x8C, 0x74, 0x94,
            0x9F, 0xEC, 0x53, 0x45, 0x48, 0xFA, 0x42, 0xB4, 0x1C, 0x15, 0xFE, 0x39, 0xD9, 0xAD, 0x4C, 0x1F,
            0x3E, 0x2F, 0x75, 0x1A, 0x39, 0xF3, 0x4F, 0x72, 0x37, 0x33, 0x0C, 0xCC, 0xEB, 0x98, 0x89, 0x32,
            0x14, 0x69, 0xE1, 0x18, 0xE3, 0x29, 0x6B, 0x8B, 0xEA, 0x89, 0x87, 0x62, 0x1D, 0x49, 0x4C, 0xA7,
            0x11, 0x2E, 0x40, 0x84, 0xF7, 0x25, 0x28, 0x46, 0x52, 0x2F, 0x84, 0xF4, 0xB9, 0x93, 0xFF, 0x94,
            0xE1, 0xC1, 0xD9, 0x3C, 0x17, 0xF2, 0xD8, 0x69, 0xF9, 0xEA, 0x8A, 0xC1, 0x85, 0xCE, 0x4F, 0x33,
            0x95, 0x73, 0x48, 0xCA, 0x11, 0x6C, 0x28, 0x98, 0x0B, 0x34, 0xAC, 0xD7, 0x2D, 0x16, 0xAB, 0x8A,
            0x22, 0x82, 0x28, 0x06, 0x67, 0xB1, 0x90, 0x44, 0x62, 0x7E, 0x69, 0xFF, 0x54, 0x54, 0xF0, 0x2B,
            0xE4, 0x46, 0x0A, 0x4E, 0x7A, 0x96, 0x6B, 0x2D, 0x74, 0xF4, 0x4D, 0x62, 0x99, 0x0A, 0xFA, 0xC4,
            0xB8, 0x5A, 0x4A, 0x23, 0xAA, 0x9B, 0xF8, 0xF4, 0xC9, 0xEF, 0xD0, 0x4A, 0x48, 0x11, 0x59, 0xEA,
            0x9F, 0xAB, 0x80, 0x93, 0x5F, 0x80, 0xE2, 0xD0, 0xDA, 0x36, 0x37, 0x2E, 0xF6, 0x27, 0xB8, 0x0D,
            0xD6, 0x0C, 0xE5, 0xD5, 0xAC, 0xA6, 0x21, 0x9D, 0xCC, 0xA9, 0xD5, 0x5D, 0x8C, 0x2A, 0x7A, 0x59,
            0x57, 0x68, 0xF0, 0x20, 0x83, 0xAD, 0xCD, 0xE0, 0x69, 0xF7, 0xB3, 0x8E, 0x60, 0x78, 0xF4, 0x6B,
            0xAE, 0xEA, 0x1B, 0x68, 0x55, 0xFB, 0xE3, 0xAB, 0x4F, 0x08, 0x5D, 0xE2, 0x94, 0x84, 0x38, 0x1B,
            0x8B, 0x96, 0x80, 0x31, 0xE4, 0x16, 0xA2, 0xB0, 0xCC, 0x72, 0x5A, 0x58, 0xE9, 0x3F, 0x74, 0xC5,
            0xFF, 0x81, 0xA1, 0x1B, 0x9B, 0x02, 0x9B, 0x8B, 0x23, 0x7A, 0x9F, 0x78, 0x32, 0xE8, 0x50, 0xDD,
            0x38, 0x49, 0x80, 0x3A, 0x5E, 0xE1, 0x98, 0x5A, 0x05, 0x9B, 0xCC, 0x78, 0x43, 0xFA, 0x81, 0xC0,
            0x72, 0x6A, 0x77, 0x5B, 0x5B, 0x9D, 0x84, 0xA6, 0xDC, 0xAA, 0x8E, 0xDD, 0x46, 0xCF, 0x99, 0x46,
            0xE8, 0xDC, 0x87, 0xCE, 0x6C, 0xA8, 0x38, 0xA1, 0x88, 0xCD, 0x8E, 0xE5, 0x8F, 0x43, 0xB3, 0x8C,
            0x74, 0x27, 0x7E, 0x60, 0x18, 0x0E, 0x4D, 0x54, 0x42, 0xD6, 0x2B, 0xE4, 0xFF, 0xC1, 0x0F, 0xCE,
            0x4A, 0x00, 0x88, 0x05, 0x07, 0x2E, 0x5A, 0x9C, 0x6E, 0x33, 0xEC, 0xD3, 0x60, 0xE8, 0x61, 0xDB
        };
    }
}