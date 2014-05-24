//------------------------------------------------------------------------------
// File: OgamaCaptureGuids.h
//
// Desc: DirectShow sample code - GUID definitions for OgamaCapture filter set
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------------------------

#pragma once

#ifndef __OGAMACAPTUREGUIDS_DEFINED
#define __OGAMACAPTUREGUIDS_DEFINED

#ifdef WIN64
// {D869F19C-5D19-4C7D-8DF1-D98A2C28C48E}
DEFINE_GUID(CLSID_OgamaCaptureDesktop, 
0xd869f19c, 0x5d19, 0x4c7d, 0x8d, 0xf1, 0xd9, 0x8a, 0x2c, 0x28, 0xc4, 0x8e);

#else
// {90B82CB3-719B-426E-AE46-28CF7A57B586}
DEFINE_GUID(CLSID_OgamaCaptureDesktop, 
0x90b82cb3, 0x719b, 0x426e, 0xae, 0x46, 0x28, 0xcf, 0x7a, 0x57, 0xb5, 0x86);
#endif

#endif
