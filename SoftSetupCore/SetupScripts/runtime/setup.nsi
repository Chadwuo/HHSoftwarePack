# ====================== 自定义宏 产品信息==============================
!define PRODUCT_NAME                "123"
!define PRODUCT_PATHNAME            "RemindDrinking"  #安装卸载项用到的KEY
!define INSTALL_APPEND_PATH         "RemindDrinking"	  #安装路径追加的名称 
!define INSTALL_DEFALT_SETUPPATH    ""       #默认生成的安装路径  
!define EXE_NAME                    "RemindDrinking.exe"
!define PRODUCT_VERSION             "1.0.0.0"  #ProductVersion必须是X.X.X.X
!define PRODUCT_PUBLISHER           "阳光心健"
!define PRODUCT_LEGAL               "阳光心健 Copyright（c）2021"
!define INSTALL_OUTPUT_NAME         "123_Setup_202101192245.exe"
# ====================== 自定义宏 安装信息==============================
!define INSTALL_7Z_PATH             "..\app.7z"
!define INSTALL_7Z_NAME             "app.7z"
!define INSTALL_RES_PATH            "skin.zip"
!define INSTALL_LICENCE_FILENAME    "license.rtf"
!define INSTALL_ICO                 "logo.ico"
!define UNINSTALL_ICO               "uninst.ico"
!include "ui_setup.nsh"
RequestExecutionLevel admin
Name "${PRODUCT_NAME}"
OutFile "..\..\..\Output\${INSTALL_OUTPUT_NAME}"
InstallDir "1"
Icon              "${INSTALL_ICO}"
UninstallIcon     "${UNINSTALL_ICO}"