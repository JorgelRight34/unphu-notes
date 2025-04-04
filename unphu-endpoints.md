## Enrolled Subjects

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/officially-enrolled-subjects/?Ano=2024&IdPersona=131394&IdPeriodo=1&IdCarrera=251`

**Data:**

- `groupSubject`
- `subjectName`
- `semester`
- `credits`
- `scheduleText`
- `teacher`
- `observation`

**Example:**

```JSON:
{
  "data": [
    {
      "groupSubjectCode": "CON-111-01",
      "subjectName": "CONTABILIDAD GENERAL I",
      "semester": 9,
      "credits": 4,
      "scheduleText": "L 20:00 - 22:00 (VIRTUAL SINCRONICA) S 19:00 - 22:00 (VIRTUAL SINCRONICA) ",
      "teacher": " ANA YANKELY ABREU",
      "observation": "AP"
    },
    {
      "groupSubjectCode": "FIS-213-01",
      "subjectName": "FÍSICA GENERAL III",
      "semester": 6,
      "credits": 3,
      "scheduleText": "J 11:00 - 13:00 (202211) M 11:00 - 13:00 (202212) ",
      "teacher": " GUSTAVO ADOLFO REGALADO",
      "observation": "RO"
    },
    {
      "groupSubjectCode": "FIS-221-01",
      "subjectName": "LABORATORIO DE FÍSICA GENERAL I",
      "semester": 4,
      "credits": 1,
      "scheduleText": "V 10:00 - 12:00 (202213) ",
      "teacher": " JULIO PAVLUSHA BAUTISTA",
      "observation": "AP"
    }
  ]
}
```

## Student Data

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/student-data/jl21-1895`

**Data:**

- `id`
- `names`
- `username`
- `career`
- `email`
- `enclosure`
- `enrollment`

**Example:**

```JSON:
{
  "data": {
    "id": 131394,
    "names": "Jorge Ernesto Lorenzo Méndez",
    "username": "jl21-1895",
    "career": "INGENIERIA EN SISTEMAS COMPUTACIONALES 255/3-15-15",
    "email": "jl21-1895@unphu.edu.do",
    "enclosure": "Santo Domingo",
    "enrollment": "21-1895"
  }
}
```

## Student Careers

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/get-student-careers/?IdPersona=131394`

**Data:**

- `IdCarrera`
- `Carrera`
- `IdPensum`
- `CodigoPensum`
- `PensumPrimario`
- `TitutloObtenidoMasculino`
- `TituloObtenidoFemenino`
- `Graduacion`
- `idestadoestudiante`
- `Nivel`
- `Transferido`
- `IdPersonaCarrera`
- `Mensaje`

**Example:**

```JSON:
{
  "data": [
    {
      "IdCarrera": 251,
      "Carrera": "INGENIERIA EN SISTEMAS COMPUTACIONALES",
      "IdPensum": 813,
      "CodigoPensum": "255/3-15-15",
      "PensumPrimario": true,
      "TituloObtenidoMasculino": "INGENIERO EN SISTEMAS COMPUTACIONALES",
      "TituloObtenidoFemenino": "INGENIERA EN SISTEMAS COMPUTACIONALES",
      "Graduacion": 0,
      "idestadoestudiante": 1,
      "Nivel": 2,
      "Transferido": 2,
      "IdPersonaCarrera": 130278,
      "Mensaje": "Para solicitar graduacion debe tener el trabajo de grado Aprobado, EC, T o I"
    }
  ]
}
```

## Subjects Elective/Optative by Career

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/subjects-elective-optative-by-career/?IdPersona=131394&IdCarrera=251`

**Data:**

- `codeElectiva`
- `subjectName`
- `codeSubject`

**Example:**

```JSON:
{
  "data": [
    {
      "codeElectiva": "ELT-001",
      "subjectName": "ACTIVIDADES ARTÍSTICAS",
      "codeSubject": "ART-100"
    },
    {
      "codeElectiva": "ELT-001",
      "subjectName": "AJEDREZ",
      "codeSubject": "EDF-101"
    },
    {
      "codeElectiva": "ELT-001",
      "subjectName": "APRECIACION DE DANZA MODERNA",
      "codeSubject": "ART-103"
    },
  ]
}
```

## Pending Grades Students

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/pending-grades-students/?IdPersona=131394&IdCarrera=251`

**Data:**

- `codeSubject`
- `subject`
- `credits`
- `codeRequired`
- `lyrics`
- `number`
- `semester`
- `noteApproval`
- `numberPeriod`
- `approvedCreditsRequired`
- `pensumCredit`
- `courses`
- `optional`
- `approved`
- `validated`
- `pending`
- `disapproved`
- `observations`

**EXAMPLE:**

```JSON:
{
  "data": [
    {
      "codeSubject": "ELT-001",
      "subject": "Electiva I (Artes y Deportes) (EDF-104 BALONCESTO)",
      "credits": 1,
      "codeRequired": "",
      "lyrics": "A",
      "number": "90",
      "semester": "SEP-DIC-2021",
      "noteApproval": 70,
      "numberPeriod": "Semestre 1  ",
      "approvedCreditsRequired": 0,
      "pensumCredit": 216,
      "courses": 157,
      "optional": 0,
      "approved": 136,
      "validated": 0,
      "pending": 59,
      "disapproved": 4,
      "observations": "AP"
    },
    {
      "codeSubject": "INF-158",
      "subject": "INTRODUCCIÓN A LA INFORMÁTICA",
      "credits": 5,
      "codeRequired": "",
      "lyrics": "B",
      "number": "82",
      "semester": "SEP-DIC-2021",
      "noteApproval": 70,
      "numberPeriod": "Semestre 1  ",
      "approvedCreditsRequired": 0,
      "pensumCredit": 216,
      "courses": 157,
      "optional": 0,
      "approved": 136,
      "validated": 0,
      "pending": 59,
      "disapproved": 4,
      "observations": "AP"
    },
  ]
}
```

## Pensum Career

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/getting-pensums-student/?idpersona=131394`

**Data:**

- `pensum`
- `career`
- `code`
- `name`
- `pensumPrimary`
- `pensumCareer`
- `studenStatusId`

**Example:**

```JSON:
{
  "data": [
    {
      "pensum": 813,
      "career": 251,
      "code": "255/3-15-15",
      "name": "INGENIERIA EN SISTEMAS COMPUTACIONALES",
      "pensumPrimary": true,
      "pensumCareer": 130278,
      "studentStatusId": 1
    }
  ]
}
```

## Unofficial Enrolled Subjects

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/unofficial-selected-subjects/?Ano=2024&IdPersona=131394&IdPeriodo=2&IdCarrera=251`

**Data:**

- `groupSubjectCode`
- `subjectName`
- `credits`
- `teacher`
- `observation`
- `semester`
- `schedule`

**Example:**

```JSON:
{
  "data": [
    {
      "groupSubjectCode": "FIS-213-12",
      "subjectName": "FÍSICA GENERAL III",
      "credits": 3,
      "teacher": " ALAN IGOR JORGE",
      "observation": "INS",
      "semester": 6,
      "schedule": "J 20:00 - 22:00 (202215) L 20:00 - 22:00 (206303) "
    },
    {
      "groupSubjectCode": "FIS-222-02",
      "subjectName": "LABORATORIO DE FÍSICA GENERAL II",
      "credits": 1,
      "teacher": " JHOEL PEGUERO",
      "observation": "INS",
      "semester": 6,
      "schedule": "MI 20:00 - 22:00 (202FI302) "
    },
    {
      "groupSubjectCode": "INF-159-01",
      "subjectName": "LENGUAJE DE PROGRAMACIÓN C",
      "credits": 3,
      "teacher": " AMBIORIX LIRIANO",
      "observation": "INS",
      "semester": 7,
      "schedule": "M 18:00 - 20:00 (VIRTUAL) MI 18:00 - 20:00 (VIRTUAL) "
    },
  ]
}
```

## Semester Grades

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/semester-grades/?Ano=2024&IdPersona=131394&IdPeriodo=1&IdCarrera=251`

**Data:**

- `course`
- `qualification`
- `careerName`
- `idEvaluationMethod`
- `letter`
- `codRubro`
- `assignedPoints`
- `points`
- `credits`
- `cumulativeIndex`
- `indexPeriod`

```JSON:
{
  "data": [
    {
      "course": "CONTABILIDAD GENERAL I",
      "qualification": 90,
      "careerName": "INGENIERIA EN SISTEMAS COMPUTACIONALES",
      "IdEvaluationMethod": 26,
      "letter": "A",
      "codRubro": "ASIS",
      "codGroup": "CON-111-01",
      "assignedPoints": "1",
      "points": 0,
      "credits": 4,
      "cumulativeIndex": 3.45,
      "indexPeriod": 4
    },
    {
      "course": "CONTABILIDAD GENERAL I",
      "qualification": 90,
      "careerName": "INGENIERIA EN SISTEMAS COMPUTACIONALES",
      "IdEvaluationMethod": 26,
      "letter": "A",
      "codRubro": "TP",
      "codGroup": "CON-111-01",
      "assignedPoints": "93",
      "points": 30.69,
      "credits": 4,
      "cumulativeIndex": 3.45,
      "indexPeriod": 4
    },
    {
      "course": "CONTABILIDAD GENERAL I",
      "qualification": 90,
      "careerName": "INGENIERIA EN SISTEMAS COMPUTACIONALES",
      "IdEvaluationMethod": 26,
      "letter": "A",
      "codRubro": "Pp1",
      "codGroup": "CON-111-01",
      "assignedPoints": "81",
      "points": 13.77,
      "credits": 4,
      "cumulativeIndex": 3.45,
      "indexPeriod": 4
    },
  ]
}
```

## Schedule Subject Selection

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/schedule-subject-selection/?Matricula=21-1895&PeriodoDesde=1&PeriodoHasta=1&IdCarrera=251&Ano=2024&IdPeriodo=2`

**Example:**

```JSON:
{
  "data": [
    {
      "NumeroPeriodo": 1,
      "Asignatura": "(ART-105) CORO (ELT-001 Electiva I (Artes y Deportes))",
      "Grupo": "01",
      "Cupo": 31,
      "Disponible": 15,
      "Profesor": "ANDRES ANTONIO CAPELLAN",
      "Dias": "VI",
      "Horario": "15-17 2051.ACT.ARTISTICA",
      "horas": 2,
      "Creditos": 1,
      "PeriodoFinal": 12
    },
    {
      "NumeroPeriodo": 1,
      "Asignatura": "(ART-106) BALLET FOLKLORICO (ELT-001 Electiva I (Artes y Deportes))",
      "Grupo": "01",
      "Cupo": 30,
      "Disponible": 26,
      "Profesor": "ANTONIA ALCANTARA",
      "Dias": "SÁ",
      "Horario": "9-11 2051.ACT.ARTISTICA",
      "horas": 2,
      "Creditos": 1,
      "PeriodoFinal": 12
    },
    {
      "NumeroPeriodo": 1,
      "Asignatura": "(ART-111) GUITARRA (ELT-001 Electiva I (Artes y Deportes))",
      "Grupo": "01",
      "Cupo": 30,
      "Disponible": 23,
      "Profesor": "SIMON CESAR",
      "Dias": "SÁ",
      "Horario": "10-12 2051.ACT.ARTISTICA",
      "horas": 2,
      "Creditos": 1,
      "PeriodoFinal": 12
    },
  ]
}
```

# Get Current Period

**Endpoint:** `https://client-api-gateway.unphusist.unphu.edu.do/legacy/get-current-period`

**Example:**

```JSON:
{
  "data": [
    {
      "id": 2,
      "name": "Mayo - Agosto",
      "year": 2024
    }
  ]
}
```
