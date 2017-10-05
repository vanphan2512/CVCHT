CREATE OR REPLACE PROCEDURE ERP.WORKSSUPORTPROJECT_V2_ROLE_ACT
(
  V_OUT OUT SYS_REFCURSOR,
  V_USERNAME IN ERP.EO_WORKSSUPPORTPROJECT_MEMBER.MEMBERUSERNAME%TYPE
)
AS
BEGIN
  IF(LOWER(V_USERNAME) = LOWER('ADMINISTRATOR')) THEN 
    OPEN V_OUT FOR
     SELECT    
        PROJECT.WORKSSUPPORTPROJECTID,
        PROJECT.WORKSSUPPORTPROJECTNAME,
        PROJECT.WORKSSUPPORTTYPEID,
        PROJECT.ISACTIVE,
        PROJECT.ISSYSTEM,
        PROJECT.ICONURL,        
            1 AS ISCANADD,
            1 AS ISCANEDIT,
            1 AS ISCANDELETE,
            1 AS ISCANVIEW,
            1 AS ISCANVIEWREPORT
      FROM
        ERP.EO_WORKSSUPPORTPROJECT PROJECT, EO_WORKSSUPPORTTYPE EW
      WHERE 
            PROJECT.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID  AND  EW.ISDELETED =0 AND 
            PROJECT.ISDELETED = 0 AND EW.ISACTIVE = 1 AND PROJECT.ISACTIVE = 1

      ORDER BY
      PROJECT.ISACTIVE DESC;
  ELSE 
    OPEN V_OUT FOR
    SELECT * 
    FROM (
            SELECT 
              PROJECT.WORKSSUPPORTPROJECTID,
                    PROJECT.WORKSSUPPORTPROJECTNAME,
                    PROJECT.WORKSSUPPORTTYPEID,
                    PROJECT.ISACTIVE,
                    PROJECT.ISSYSTEM,
                    PROJECT.ICONURL,
                    0 AS ISCANADD,
                    0 AS ISCANEDIT,
                    0 AS ISCANDELETE,
                    1 AS ISCANVIEW,
                    0 AS ISCANVIEWREPORT
               FROM EO_WORKSSUPPORTPROJECT_MEMBER PROJECT_MEMBER,
                    EO_WORKSSUPPORTPROJECT PROJECT, 
                    EO_WORKSSUPPORTTYPE EW
               WHERE PROJECT.WORKSSUPPORTPROJECTID = PROJECT_MEMBER.WORKSSUPPORTPROJECTID AND 
                   PROJECT_MEMBER.MEMBERUSERNAME = V_USERNAME AND  PROJECT.ISDELETED = 0 AND EW.ISACTIVE = 1 AND
                  PROJECT_MEMBER.ISDELETED = 0 AND  EW.ISDELETED =0 AND PROJECT.ISACTIVE = 1 AND 
                     NOT EXISTS(
                             SELECT    
                                PROJECT.WORKSSUPPORTPROJECTID                   
                              FROM
                                ERP.EO_WORKSSUPPORTPROJECT PROJECT, 
                                EO_WORKSSUPPORTTYPE EW, 
                                EO_WORKSSUPPORTPROJECT_PERMIS EWP
                              WHERE 
                                    PROJECT.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID AND
                                    EW.WORKSSUPPORTTYPEID = EWP.WORKSSUPPORTTYPEID AND 
                                    EWP.USERNAME = V_USERNAME  AND PROJECT.ISDELETED = 0 AND 
                                    PROJECT_MEMBER.WORKSSUPPORTPROJECTID = PROJECT.WORKSSUPPORTPROJECTID 
                               )
                 UNION
                  SELECT    
                    PROJECT.WORKSSUPPORTPROJECTID,
                    PROJECT.WORKSSUPPORTPROJECTNAME,
                    PROJECT.WORKSSUPPORTTYPEID,
                    PROJECT.ISACTIVE,
                    PROJECT.ISSYSTEM,
                    PROJECT.ICONURL,
                    EWP.ISCANADDPROJECT AS ISCANADD,
                    EWP.ISCANEDITPROJECT AS ISCANEDIT,
                    EWP.ISCANDELETEPROJECT AS ISCANDELETE,
                    EWP.ISCANVIEWPROJECT AS ISCANVIEW,
                    EWP.ISCANVIEWPROJECTREPORT AS ISCANVIEWREPORT
                  FROM
                    ERP.EO_WORKSSUPPORTPROJECT PROJECT,
                    EO_WORKSSUPPORTTYPE EW,
                    EO_WORKSSUPPORTPROJECT_PERMIS EWP
                  WHERE 
                        PROJECT.WORKSSUPPORTTYPEID = EW.WORKSSUPPORTTYPEID AND
                        EW.WORKSSUPPORTTYPEID = EWP.WORKSSUPPORTTYPEID AND EW.ISDELETED =0 AND
                        EWP.USERNAME = V_USERNAME  AND PROJECT.ISDELETED = 0 AND EW.ISACTIVE = 1 AND PROJECT.ISACTIVE = 1
              ) WSS
          ORDER BY WSS.ISACTIVE DESC;
  END IF;
END;
/