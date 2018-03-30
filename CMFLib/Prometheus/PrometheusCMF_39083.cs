﻿using static CMFLib.CMFHandler;

namespace CMFLib.Prometheus {
    [CMFMetadata(AutoDetectVersion = true, BuildVersions = new uint[] { }, App = CMFApplication.Prometheus)]
    public class PrometheusCMF_39083 : ICMFProvider {
        public byte[] Key(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];

            uint kidx = Constrain(length * header.BuildVersion);
            uint decrement = header.BuildVersion & 511;
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx -= decrement;
            }

            return buffer;
        }

        public byte[] IV(CMFHeaderCommon header, string name, byte[] digest, int length) {
            byte[] buffer = new byte[length];
            uint kidx = Keytable[header.DataCount & 511];
            for (int i = 0; i != length; ++i) {
                buffer[i] = Keytable[kidx % 512];
                kidx += 3;
                buffer[i] ^= digest[(kidx - i) % SHA1_DIGESTSIZE];
            }

            return buffer;
        }

        private static readonly byte[] Keytable = {
            0x16, 0x7A, 0x47, 0x4E, 0x71, 0x7B, 0x99, 0x57, 0x74, 0xB8, 0xD8, 0xBA, 0x67, 0x00, 0x3F, 0x7D,
            0xED, 0x88, 0xC1, 0x6A, 0x70, 0x8F, 0xA0, 0x2C, 0x67, 0x19, 0x1F, 0xE0, 0xAE, 0xDF, 0xB2, 0x5A,
            0xEE, 0x7C, 0x54, 0x40, 0x6C, 0xEE, 0x0D, 0x26, 0x13, 0xBB, 0x3F, 0x90, 0xC4, 0x5E, 0x66, 0x7F,
            0xC6, 0x04, 0x8B, 0x98, 0xC0, 0x71, 0x99, 0x60, 0xE9, 0x33, 0x28, 0xC0, 0x1E, 0xB3, 0x25, 0xD4,
            0xD2, 0x8B, 0x43, 0xE8, 0x28, 0x7A, 0x50, 0x08, 0x45, 0xD3, 0xC2, 0xC1, 0xDE, 0x3B, 0xD2, 0xFF,
            0x26, 0x7F, 0x2E, 0xF3, 0xB1, 0x39, 0xA5, 0x82, 0x97, 0x32, 0x20, 0xC4, 0x8E, 0xDE, 0xA0, 0xCC,
            0x67, 0x3D, 0x16, 0x2C, 0x85, 0x8E, 0xCA, 0xE4, 0x44, 0xC8, 0x07, 0x6D, 0xF5, 0xAF, 0x9B, 0x67,
            0xE9, 0xBA, 0x90, 0x16, 0xF8, 0xE0, 0xF9, 0x49, 0x18, 0xB6, 0x3B, 0xC8, 0x10, 0x7B, 0x0C, 0xF2,
            0x0F, 0x00, 0x3A, 0xC3, 0xD8, 0x19, 0xDD, 0xC0, 0x39, 0x29, 0xAD, 0x5F, 0xDB, 0x95, 0x31, 0xF7,
            0x44, 0xC3, 0x84, 0x8E, 0x51, 0xFA, 0x94, 0x87, 0x5A, 0xF2, 0x9C, 0xA9, 0x69, 0x53, 0x28, 0xD7,
            0xA0, 0xF8, 0xF4, 0x8D, 0x04, 0xB4, 0xC7, 0xC4, 0xE8, 0xF0, 0x91, 0x6E, 0x16, 0x0E, 0xAF, 0x6B,
            0x2E, 0xB7, 0x85, 0x88, 0x58, 0x95, 0xC0, 0x21, 0xE9, 0x13, 0x56, 0x5B, 0x88, 0xA1, 0x72, 0x71,
            0xCB, 0x54, 0x37, 0x28, 0xBB, 0xCF, 0x2D, 0xD8, 0x68, 0xB8, 0x15, 0xF3, 0x30, 0xB4, 0x45, 0xA5,
            0x95, 0x56, 0x69, 0x96, 0x74, 0x39, 0xE2, 0x9A, 0xA4, 0x55, 0xD1, 0x8D, 0x02, 0x5B, 0x91, 0x25,
            0xFC, 0x4A, 0xB3, 0xE0, 0x9E, 0x72, 0x73, 0x00, 0xCA, 0x03, 0x41, 0xBF, 0x5D, 0x19, 0x8F, 0x4E,
            0x59, 0xA4, 0x40, 0x23, 0x50, 0x12, 0xDE, 0xB1, 0x4B, 0xC3, 0x6C, 0x8A, 0x2C, 0xF8, 0xCF, 0x5E,
            0xED, 0x0C, 0x7D, 0xBE, 0x68, 0xE8, 0xCC, 0x46, 0xDC, 0xA1, 0xC7, 0x37, 0xCA, 0x36, 0x62, 0x7F,
            0x5B, 0xBD, 0x1A, 0xAE, 0xAC, 0x8C, 0x32, 0xFE, 0x7A, 0xD5, 0x72, 0xD4, 0x7C, 0x98, 0x9A, 0xB4,
            0xBB, 0x0C, 0xA2, 0x23, 0x45, 0x6B, 0x2A, 0xFC, 0xAE, 0xE6, 0xF2, 0x85, 0x06, 0x21, 0x98, 0x33,
            0x59, 0xD5, 0x06, 0x1B, 0x4B, 0xA9, 0x61, 0xCD, 0x66, 0x35, 0x56, 0xD8, 0x22, 0xEF, 0x7E, 0xAB,
            0x1C, 0x58, 0xE9, 0x98, 0xB5, 0xE0, 0x85, 0xEC, 0xCB, 0x3A, 0xA8, 0x6D, 0x87, 0x96, 0xA8, 0x84,
            0xA2, 0x6C, 0xDD, 0x4F, 0x06, 0x5C, 0xE4, 0x03, 0x47, 0xAF, 0x19, 0x00, 0x4B, 0x2B, 0xE4, 0x1A,
            0x60, 0x68, 0x2A, 0x70, 0xC5, 0x48, 0x68, 0xA7, 0x41, 0xE2, 0xE2, 0xC3, 0x51, 0xF4, 0xED, 0x58,
            0xCF, 0x63, 0x44, 0x6B, 0x3C, 0xC8, 0x18, 0x46, 0xCC, 0x89, 0x5D, 0x01, 0x3C, 0x42, 0x6F, 0x3B,
            0xDE, 0xAA, 0xCC, 0x51, 0x09, 0xCC, 0xF9, 0x74, 0x10, 0x86, 0xB8, 0x96, 0xD5, 0x69, 0xD3, 0x42,
            0xB1, 0x4B, 0xD9, 0x05, 0x8E, 0x15, 0x77, 0xC2, 0x09, 0x50, 0xA6, 0x6F, 0xD0, 0xC2, 0xB6, 0xA6,
            0xC6, 0xC6, 0x02, 0x70, 0xAD, 0xBA, 0x3B, 0x67, 0x89, 0xF5, 0xFC, 0x93, 0xE8, 0x52, 0xCC, 0x0C,
            0x62, 0x1C, 0x14, 0x10, 0x6F, 0x6D, 0x1D, 0x98, 0xB6, 0xDF, 0x82, 0x3F, 0x01, 0x33, 0x16, 0x67,
            0xF6, 0xFE, 0x8B, 0x8F, 0x57, 0x9B, 0x7D, 0x7B, 0x00, 0x2E, 0xE2, 0xEC, 0x85, 0x60, 0x93, 0xD9,
            0x21, 0x1D, 0x95, 0xFE, 0x41, 0x1E, 0x14, 0xB0, 0x3F, 0xD1, 0x55, 0x5E, 0x1E, 0xA9, 0xC0, 0x86,
            0x93, 0xAD, 0x36, 0x5D, 0x23, 0xBA, 0x83, 0x30, 0x78, 0x4C, 0x2F, 0x29, 0x14, 0xBE, 0x4A, 0xEF,
            0xDE, 0x54, 0xAE, 0xB9, 0x97, 0xAA, 0x9B, 0x8A, 0xE9, 0xE0, 0xAB, 0xCE, 0x22, 0x0D, 0x0C, 0xE2
        };
    }
}