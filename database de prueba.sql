-- Creaci�n de la tabla Roles
select * from Roles
  

-- Inserciones en la tabla Roles
INSERT INTO Roles ( NameRol, Description) VALUES
( 'Doctor', 'Especialista en medicina general'),
('Paciente', 'Persona que recibe atenci�n m�dica'),
( 'Administrador', 'Encargado de gestionar el sistema'),
('Enfermero', 'Personal de enfermer�a'),
('Recepcionista', 'Personal de recepci�n');

-- Creaci�n de la tabla Users

-- Inserciones en la tabla Users
INSERT INTO Users (FirstName, LastName, Birth, UserName, Password, RolId) VALUES
('Juan', 'P�rez', '1985-06-10', 'jperez', 'password123', 1),
('Ana', 'G�mez', '1990-04-15', 'agomez', 'password456', 2),
('Carlos', 'Rodr�guez', '1980-11-22', 'crodriguez', 'password789', 1),
('Mar�a', 'L�pez', '1975-02-28', 'mlopez', 'password012', 3),
('Jos�', 'Mart�nez', '1995-09-30', 'jmartinez', 'password345', 2);

-- Creaci�n de la tabla Medications


-- Inserciones en la tabla Medications
INSERT INTO Medications (CommercialName, ScientificName, [Group], Description, Laboratory) VALUES
('Paracetamol', 'Acetaminof�n', 'Analg�sico y antipir�tico', 'Alivia el dolor y la fiebre', 'Laboratorio A'),
('Ibuprofeno', 'Ibuprofenum', 'Antiinflamatorio', 'Reduce la inflamaci�n y el dolor', 'Laboratorio B'),
('Amoxicilina', 'Amoxicillin', 'Antibi�tico', 'Trata infecciones bacterianas', 'Laboratorio C'),
('Diazepam', 'Diazepam', 'Ansiol�tico', 'Reduce la ansiedad y relaja los m�sculos', 'Laboratorio D'),
('Omeprazol', 'Omeprazolum', 'Antibi�tico', 'Reduce la producci�n de �cido g�strico', 'Laboratorio E'),
('Lactulosa', 'Lactulose', 'Laxante', 'Alivia el estre�imiento', 'Laboratorio F'),
('Sen�sidos', 'Sennosides', 'Laxante', 'Estimula los movimientos intestinales', 'Laboratorio G'),
('Lorazepam', 'Lorazepam', 'Ansiol�tico', 'Trata la ansiedad a corto plazo', 'Laboratorio H'),
('Simvastatina', 'Simvastatin', 'Reductor de colesterol', 'Disminuye los niveles de colesterol en la sangre', 'Laboratorio I'),
('Salbutamol', 'Salbutamol', 'Broncodilatador', 'Abre las v�as respiratorias en casos de asma', 'Laboratorio J');




-- Inserciones en la tabla Appointments
INSERT INTO Appoiments(Time, Date, UserPatientId, UserDoctorId) VALUES
('10:00', '2024-10-05', 1, 2),
('11:00', '2024-10-06', 2, 3),
('12:00', '2024-10-07', 3, 4),
('13:00', '2024-10-08', 4, 5),
('14:00', '2024-10-09', 5, 1);

-- Creaci�n de la tabla MedicalSpe

-- Inserciones en la tabla MedicalSpe
INSERT INTO MedicalSpe (Name, UserDoctorId) VALUES
( 'Cardiolog�a', 1),
( 'Neurolog�a', 2),
( 'Dermatolog�a', 3),
( 'Ginecolog�a', 4),
( 'Pediatr�a', 5);

-- Inserciones en la tabla MedicalHistory
INSERT INTO MedicalHistory ( Description, NamePatient, AppoimentId) VALUES
( 'Historial de consulta 1', 'Juan P�rez', 1),
( 'Historial de consulta 2', 'Ana G�mez', 2),
( 'Historial de consulta 3', 'Carlos Rodr�guez', 3),
( 'Historial de consulta 4', 'Mar�a L�pez', 4),
( 'Historial de consulta 5', 'Jos� Mart�nez', 5);



-- Inserciones en la tabla Status
INSERT INTO Status ( StatusAppoiment, AppoimentId) VALUES
( 'Pendiente', 1),
( 'Completado', 2),
( 'Cancelado', 3),
( 'En progreso', 4),
( 'Reprogramado', 5);


-- Inserciones en la tabla MedicalOrders
INSERT INTO MedicalOrders (Description, Diagnosis, IdMedication, IdAppoiment,MedicationId,IdAppoiment) VALUES
( 'Orden de antibi�ticos', 'Infecci�n respiratoria', 1, 1, 1, 1),
( 'Orden de analg�sicos', 'Dolor cr�nico', 2, 2, 2, 2),
( 'Orden de radiograf�a', 'Dolor en el pecho', 3, 3, 3, 3),
( 'Orden de fisioterapia', 'Lesi�n muscular', 4, 4, 4, 4),
( 'Orden de control', 'Consulta de seguimiento', 5, 5, 5, 5);
	
INSERT INTO MedicalOrders (Description, Diagnosis, MedicationId, IdAppoiment) 
VALUES
('Orden de antibi�ticos', 'Infecci�n respiratoria', 1, 1),
('Orden de analg�sicos', 'Dolor cr�nico', 2, 2),
('Orden de radiograf�a', 'Dolor en el pecho', 3, 3),
('Orden de fisioterapia', 'Lesi�n muscular', 4, 4),
('Orden de control', 'Consulta de seguimiento', 5, 5);


