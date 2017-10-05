CREATE OR REPLACE PROCEDURE ERP.WORKSSUPORTGROUP_V2_CHK_USER
/*
	CREATED BY	:	NGUY?N VAN HUÂN 
	DATE		:	09/08/2017
	DESCRIPTION	:	KI?M TRA THÀNH VIÊN TRONG NHÓM CÔNG VI?C CÓ TH?C HI?N BU?C X? LÝ NÀO KHÔNG.
*/
(
	V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTGROUPID%TYPE,
  V_PROCESSUSER IN EO_WORKSSUPPORT_WORKFLOW.PROCESSUSER%TYPE,
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
  SELECT COUNT(1) AS CHECKPROCESSUSER  
    FROM  EO_WORKSSUPPORT_WORKFLOW EWW
    WHERE
          EWW.PROCESSUSER = V_PROCESSUSER  AND EWW.ISPROCESS = 0  AND 
          EXISTS (SELECT 
                      EW.WORKSSUPPORTID 
                    FROM 
                      EO_WORKSSUPPORT EW 
                    WHERE  
                      EW.ISDELETED =0 AND
                      EXISTS (SELECT 
                                EW1.WORKSSUPPORTGROUPID 
                               FROM 
                                  EO_WORKSSUPPORTGROUP EW1
                               WHERE 
                                 trim(upper(EW1.workssupportgroupid)) = trim(upper(v_workssupportgroupid)) AND 
                                  EW1.ISDELETED =0
                              )
                   );
END;
/