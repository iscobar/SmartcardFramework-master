﻿/**
 * @author Olivier ROUIT
 * 
 * @license CPL, CodeProject license 
 */

namespace Core.Smartcard.Mifare
{
    public class MifareParam
    {
        public const byte
            CLASS = 0xFF,
            INS_AUTH_BLOCK = 0x86,
            INS_READ_BLOCKS = 0xB0,
            INS_UPDATE_BLOCKS = 0xD6,
            INS_VALUE_BLOCK_OPER = 0xD7,
            INS_READ_VALUE_BLOCK = 0xB1,
            KEY_TYPE_A = 0x60,
            KEY_TYPE_B = 0x61;
    }
}
