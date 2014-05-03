﻿using System;
using System.Runtime.InteropServices;

using CXIdxClientFile = System.IntPtr; // void*
using CXIdxClientEntity = System.IntPtr; // void*
using CXIdxClientContainer = System.IntPtr; // void*
using CXIdxClientASTFile = System.IntPtr; // void*
using CXIndexAction = System.IntPtr; // void*

using CXIdxObjCContainerDeclInfoPtr = System.IntPtr; // CXIdxObjCContainerDeclInfo *
using CXIdxObjCInterfaceDeclInfoPtr = System.IntPtr; // CXIdxObjCInterfaceDeclInfo *
using CXIdxObjCCategoryDeclInfoPtr = System.IntPtr; // CXIdxObjCCategoryDeclInfo *
using CXIdxObjCProtocolRefListInfoPtr = System.IntPtr; // CXIdxObjCProtocolRefListInfo *
using CXIdxObjCPropertyDeclInfoPtr = System.IntPtr; // const CXIdxObjCPropertyDeclInfo *
using CXIdxIBOutletCollectionAttrInfoPtr = System.IntPtr; // const CXIdxIBOutletCollectionAttrInfo *
using CXIdxCXXClassDeclInfoPtr = System.IntPtr; // const CXIdxCXXClassDeclInfo *

using CXIndex = System.IntPtr; // void*
using CXFile = System.IntPtr;
using CXModule = System.IntPtr; // void*
using CXClientData = System.IntPtr; // void*
using CXDiagnosticSet = System.IntPtr; // void*
using CXTranslationUnit = System.IntPtr; // CXTranslationUnitImpl*

namespace NClang.Natives
{
	delegate VisitorResult CXVisitorResultVisitor (IntPtr context, CXCursor _, CXSourceRange __);

	[StructLayout (LayoutKind.Sequential)]
	struct CXCursorAndRangeVisitor
	{
		internal CXCursorAndRangeVisitor (CXVisitorResultVisitor visit)
		{
			Context = IntPtr.Zero;
			Visit = visit;
		}

		public readonly IntPtr Context;
		public readonly CXVisitorResultVisitor Visit;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxLoc
	{
		public IntPtr PtrData1;
		public IntPtr PtrData2;
		[MarshalAs (UnmanagedType.SysUInt)]
		public uint IntData;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxIncludedFileInfo
	{
		public CXIdxLoc HashLoc;
		public string Filename;
		public CXFile File;
		public int IsImport;
		public int IsAngled;
		public int IsModuleImport;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxImportedASTFileInfo
	{
		public CXIdxImportedASTFileInfo (CXIdxClientFile file, CXIdxClientFile module, CXIdxLoc loc, int isImplicit)
		{
			this.File = file;
			this.Module = module;
			this.Loc = loc;
			this.IsImplicit = isImplicit;
		}		

		public readonly CXFile File;
		public readonly CXModule Module;
		public readonly CXIdxLoc Loc;
		public readonly int IsImplicit;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxAttrInfo
	{
		public IndexAttributeKind Kind;
		public CXCursor Cursor;
		public CXIdxLoc Loc;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxEntityInfo
	{
		public IndexEntityKind Kind;
		public IndexEntityCxxTemplateKind CxxTemplateKind;
		public IndexEntityLanguage Lang;
		public string Name;
		public string USR;
		public CXCursor Cursor;
		public IntPtr Attributes; // const CXIdxAttrInfo *const *
		[MarshalAs (UnmanagedType.SysUInt)]
		public uint NumAttributes;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxContainerInfo
	{
		public CXCursor Cursor;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxIBOutletCollectionAttrInfo
	{
		public IntPtr AttrInfo; // const CXIdxAttrInfo *
		public IntPtr ObjcClass; // const CXIdxEntityInfo *
		public CXCursor ClassCursor;
		public CXIdxLoc ClassLoc;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxDeclInfo
	{
		public IntPtr EntityInfo; // const CXIdxEntityInfo *
		public CXCursor Cursor;
		public CXIdxLoc Loc;
		public IntPtr SemanticContainer; // const CXIdxContainerInfo *
		public IntPtr LexicalContainer; // const CXIdxContainerInfo *
		[MarshalAs (UnmanagedType.SysInt)]
		public int IsRedeclaration;
		[MarshalAs (UnmanagedType.SysInt)]
		public int IsDefinition;
		[MarshalAs (UnmanagedType.SysInt)]
		public int IsContainer;
		public IntPtr DeclAsContainer; // const CXIdxContainerInfo *
		[MarshalAs (UnmanagedType.SysInt)]
		public int IsImplicit;
		public IntPtr Attributes; // const CXIdxAttrInfo *const *
		[MarshalAs (UnmanagedType.SysUInt)]
		public uint NumAttributes;
		[MarshalAs (UnmanagedType.SysUInt)]
		public uint Flags;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCContainerDeclInfo
	{
		public IntPtr DeclInfo; // const CXIdxDeclInfo *
		public IndexObjCContainerKind Kind;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxBaseClassInfo
	{
		public IntPtr Base; //const CXIdxEntityInfo *
		public CXCursor Cursor;
		public CXIdxLoc Loc;
	}

	
	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCProtocolRefInfo
	{
		public IntPtr Protocol; //const CXIdxEntityInfo *
		public CXCursor Cursor;
		public CXIdxLoc Loc;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCProtocolRefListInfo
	{
		public IntPtr Protocols; // const CXIdxObjCProtocolRefInfo *const *
		public uint NumProtocols;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCInterfaceDeclInfo
	{
		public CXIdxObjCContainerDeclInfoPtr ContainerInfo;
		public IntPtr	SuperInfo; // const CXIdxBaseClassInfo *
		public CXIdxObjCProtocolRefListInfoPtr	Protocols;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCCategoryDeclInfo
	{
		public CXIdxObjCContainerDeclInfoPtr ContainerInfo;
		public IntPtr ObjcClass; // const CXIdxEntityInfo *
		public CXCursor ClassCursor;
		public CXIdxLoc ClassLoc;
		public CXIdxObjCProtocolRefListInfoPtr Protocols;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxObjCPropertyDeclInfo
	{
		public IntPtr DeclInfo; // const CXIdxDeclInfo *
		public IntPtr Getter; // const CXIdxEntityInfo *
		public IntPtr Setter; // const CXIdxEntityInfo *
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxCXXClassDeclInfo
	{
		public IntPtr DeclInfo; // const CXIdxDeclInfo *
		public IntPtr Bases; // const CXIdxBaseClassInfo *const *
		public uint NumBases;
	}

	[StructLayout (LayoutKind.Sequential)]
	struct CXIdxEntityRefInfo
	{
		public IndexEntityRefKind Kind;
		public CXCursor Cursor;
		public CXIdxLoc Loc;
		public IntPtr ReferencedEntity; // const CXIdxEntityInfo *
		public IntPtr ParentEntity; // const CXIdxEntityInfo *
		public IntPtr Container; // const CXIdxContainerInfo *
	}
	
	[return:MarshalAs (UnmanagedType.SysInt)]
	delegate int AbortQueryHandler (CXClientData client_data,IntPtr reserved);
	delegate void DiagnosticHandler (CXClientData client_data,CXDiagnosticSet _,IntPtr reserved);
	delegate CXIdxClientFile EnteredMainFileHandler (CXClientData client_data,CXFile mainFile,IntPtr reserved);
	delegate CXIdxClientFile PpIncludedFileHandler (CXClientData client_data,ref CXIdxIncludedFileInfo _);
	delegate CXIdxClientASTFile ImportedASTFileHandler (CXClientData client_data,ref CXIdxImportedASTFileInfo _);
	delegate CXIdxClientContainer StartedTranslationUnitHandler (CXClientData client_data,IntPtr reserved);
	delegate void IndexDeclarationHandler (CXClientData client_data, IntPtr _); //  CXIdxDeclInfo*
	delegate void IndexEntityReferenceHandler (CXClientData client_data, IntPtr _); // CXIdxEntityRefInfo*

	[StructLayout (LayoutKind.Sequential)]
	struct IndexerCallbacks
	{
		public AbortQueryHandler AbortQuery;
		public DiagnosticHandler Diagnostic;
		public EnteredMainFileHandler EnteredMainFile;
		public PpIncludedFileHandler PpIncludedFile;
		public ImportedASTFileHandler ImportedASTFile;
		public StartedTranslationUnitHandler StartedTranslationUnit;
		public IndexDeclarationHandler IndexDeclaration;
		public IndexEntityReferenceHandler IndexEntityReference;
	}

	// done
	static partial class LibClang
	{
		[DllImport (LibraryName)]
		 internal static extern FindResult 	clang_findReferencesInFile (CXCursor cursor, CXFile file, CXCursorAndRangeVisitor visitor);

		[DllImport (LibraryName)]
		 internal static extern FindResult 	clang_findIncludesInFile (CXTranslationUnit TU, CXFile file, CXCursorAndRangeVisitor visitor);

		[return:MarshalAs (UnmanagedType.SysInt)]
		[DllImport (LibraryName)]
		 internal static extern int 	clang_index_isEntityObjCContainerKind (IndexEntityKind _);

		[DllImport (LibraryName)]
		 internal static extern CXIdxObjCContainerDeclInfoPtr clang_index_getObjCContainerDeclInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxObjCInterfaceDeclInfoPtr 	clang_index_getObjCInterfaceDeclInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxObjCCategoryDeclInfoPtr 	clang_index_getObjCCategoryDeclInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxObjCProtocolRefListInfoPtr 	clang_index_getObjCProtocolRefListInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxObjCPropertyDeclInfoPtr 	clang_index_getObjCPropertyDeclInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxIBOutletCollectionAttrInfoPtr 	clang_index_getIBOutletCollectionAttrInfo (IntPtr _); // const CXIdxAttrInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxCXXClassDeclInfoPtr 	clang_index_getCXXClassDeclInfo (IntPtr _); // const CXIdxDeclInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxClientContainer 	clang_index_getClientContainer (IntPtr _); // const CXIdxContainerInfo *
		[DllImport (LibraryName)]
		 internal static extern void 	clang_index_setClientContainer (IntPtr _, CXIdxClientContainer __); // const CXIdxContainerInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIdxClientEntity 	clang_index_getClientEntity (IntPtr _); // const CXIdxEntityInfo *
		[DllImport (LibraryName)]
		 internal static extern void 	clang_index_setClientEntity (IntPtr _, CXIdxClientEntity __); // const CXIdxEntityInfo *
		[DllImport (LibraryName)]
		 internal static extern CXIndexAction 	clang_IndexAction_create (CXIndex CIdx);

		[DllImport (LibraryName)]
		 internal static extern void 	clang_IndexAction_dispose (CXIndexAction _);

		[return:MarshalAs (UnmanagedType.SysInt)]
		[DllImport (LibraryName)]
		 internal static extern int 	clang_indexSourceFile (CXIndexAction _, CXClientData client_data,
			[MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 3)] IndexerCallbacks [] index_callbacks,
			[MarshalAs (UnmanagedType.SysUInt)] uint index_callbacks_size,
			[MarshalAs (UnmanagedType.SysUInt)] IndexOptFlags index_options,
			string source_filename,
			[MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 7)] string [] command_line_args, // const char *const *
			[MarshalAs (UnmanagedType.SysInt)] int num_command_line_args,
			[MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 9)] CXUnsavedFile[] unsaved_files,
			[MarshalAs (UnmanagedType.SysUInt)] uint num_unsaved_files,
			out CXTranslationUnit out_TU, [MarshalAs (UnmanagedType.SysUInt)] TranslationUnitFlags TU_options);

		[return:MarshalAs (UnmanagedType.SysInt)]
		[DllImport (LibraryName)]
		 internal static extern int 	clang_indexTranslationUnit (CXIndexAction _, CXClientData client_data,
			[MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 3)] IndexerCallbacks [] index_callbacks, [MarshalAs (UnmanagedType.SysUInt)] uint index_callbacks_size,
			[MarshalAs (UnmanagedType.SysUInt)] IndexOptFlags index_options, CXTranslationUnit __);

		[DllImport (LibraryName)]
		internal static extern void 	clang_indexLoc_getFileLocation (CXIdxLoc loc, out IntPtr indexFile, out IntPtr file, // CXIdxClientFile*, CXIdxClientFile
			out uint line, out uint column, out uint offset);

		[DllImport (LibraryName)]
		 internal static extern CXSourceLocation 	clang_indexLoc_getCXSourceLocation (CXIdxLoc loc);
	}
}
