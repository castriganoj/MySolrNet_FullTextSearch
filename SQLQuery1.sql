SELECT sOrgPath, sName, a.*
FROM tracer.aud_correctiveaction a
INNER JOIN tracer.aud_finding f ON a.IAuditFileID = f.IAuditFileID  AND a.ICorrActionSN = f.IFindingID 
INNER JOIN tracer.AuditFile ad ON f.IAuditFileID = ad.IAuditFileID
INNER JOIN tracer.Status s ON s.IStatusID = a.sStatus

