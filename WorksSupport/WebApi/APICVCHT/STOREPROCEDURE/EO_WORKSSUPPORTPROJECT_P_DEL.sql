create or replace PROCEDURE     EO_WORKSSUPPORTPROJECT_P_DEL
/*
	Created by	:	L��ng Trung Nh�n
	Date		:	12/06/2017
	Description	:	X�a th�ng tin quy?n tr�n d? �n c�ng vi?c c?n h? tr?
*/
(
	V_WORKSSUPPORTTYPEID IN EO_WORKSSUPPORTPROJECT_PERMIS.WORKSSUPPORTTYPEID%TYPE
)
AS
BEGIN
	DELETE 
        EO_WORKSSUPPORTPROJECT_PERMIS
	WHERE
		WORKSSUPPORTTYPEID = V_WORKSSUPPORTTYPEID;
END;