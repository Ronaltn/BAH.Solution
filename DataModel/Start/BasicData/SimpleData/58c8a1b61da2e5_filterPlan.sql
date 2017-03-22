IF NOT EXISTS (SELECT 1 FROM T_BAS_FILTERSCHEME WHERE FSCHEMEID='58c8a1b61da2e5')
BEGIN
/****** Object:Data       Script Date: 2017-03-22 14:04:27 ******/
DELETE T_BAS_FILTERSCHEME WHERE FSCHEMEID='58c8a1b61da2e5' 
INSERT INTO T_BAS_FILTERSCHEME(FSCHEMEID,FFORMID,FSCHEMENAME,FUSERID,FISDEFAULT,FSCHEME,FISSHARE,FNEXTENTRYSCHEME,FSEQ) VALUES ('58c8a1b61da2e5','SEC_PermissionItem',N'不受组织控制基础资料',16394,'0',null,0,'0',20)  

/****** Object:Data       Script Date: 2017-03-22 14:04:27 ******/
DELETE T_BAS_FILTERSCHEME_L WHERE FSCHEMEID='58c8a1b61da2e5' 
INSERT INTO T_BAS_FILTERSCHEME_L(FPKID,FSCHEMEID,FLOCALEID,FDESCRIPTION) VALUES ('58c8a1b61da2e6','58c8a1b61da2e5',2052,N'不受组织控制基础资料')  

END;

