
select * from Roles
-- Inserciones en la tabla Roles e
INSERT INTO Roles ( NameRol, Description) VALUES
( 'Doctor', 'Especialista en medicina general'),
('Paciente', 'Persona que recibe atención médica'),
( 'Administrador', 'Encargado de gestionar el sistema'),
('Enfermero', 'Personal de enfermería'),
('Recepcionista', 'Personal de recepción');

-- Inserciones en la tabla Users
INSERT INTO Users (FirstName, LastName, Birth, UserName, Password, RolId) VALUES
('Juan', 'Pérez', '1985-06-10', 'jperez', 'password123', 1),
('Ana', 'Gómez', '1990-04-15', 'agomez', 'password456', 2),
('Carlos', 'Rodríguez', '1980-11-22', 'crodriguez', 'password789', 1),
('María', 'López', '1975-02-28', 'mlopez', 'password012', 3),
('José', 'Martínez', '1995-09-30', 'jmartinez', 'password345', 2);

-- Inserciones en la tabla Medications
INSERT INTO Medications (CommercialName, ScientificName, [Group], Description, Laboratory) VALUES
('Paracetamol', 'Acetaminofén', 'Analgésico y antipirético', 'Alivia el dolor y la fiebre', 'Laboratorio A'),
('Ibuprofeno', 'Ibuprofenum', 'Antiinflamatorio', 'Reduce la inflamación y el dolor', 'Laboratorio B'),
('Amoxicilina', 'Amoxicillin', 'Antibiótico', 'Trata infecciones bacterianas', 'Laboratorio C'),
('Diazepam', 'Diazepam', 'Ansiolítico', 'Reduce la ansiedad y relaja los músculos', 'Laboratorio D'),
('Omeprazol', 'Omeprazolum', 'Antibiótico', 'Reduce la producción de ácido gástrico', 'Laboratorio E'),
('Lactulosa', 'Lactulose', 'Laxante', 'Alivia el estreñimiento', 'Laboratorio F'),
('Senósidos', 'Sennosides', 'Laxante', 'Estimula los movimientos intestinales', 'Laboratorio G'),
('Lorazepam', 'Lorazepam', 'Ansiolítico', 'Trata la ansiedad a corto plazo', 'Laboratorio H'),
('Simvastatina', 'Simvastatin', 'Reductor de colesterol', 'Disminuye los niveles de colesterol en la sangre', 'Laboratorio I'),
('Salbutamol', 'Salbutamol', 'Broncodilatador', 'Abre las vías respiratorias en casos de asma', 'Laboratorio J');


-- Inserciones en la tabla Appointments
INSERT INTO Appoiments(Time, Date, UserPatientId, UserDoctorId) VALUES
('10:00', '2024-10-05', 1, 2),
('11:00', '2024-10-06', 2, 3),
('12:00', '2024-10-07', 3, 4),
('13:00', '2024-10-08', 4, 5),
('14:00', '2024-10-09', 5, 1);

-- Inserciones en la tabla MedicalSpe
INSERT INTO MedicalSpe (Name, UserDoctorId) VALUES
( 'Cardiología', 1),
( 'Neurología', 2),
( 'Dermatología', 3),
( 'Ginecología', 4),
( 'Pediatría', 5);

-- Inserciones en la tabla MedicalHistory
INSERT INTO MedicalHistory ( Description, NamePatient, AppoimentId) VALUES
( 'Historial de consulta 1', 'Juan Pérez', 1),
( 'Historial de consulta 2', 'Ana Gómez', 2),
( 'Historial de consulta 3', 'Carlos Rodríguez', 3),
( 'Historial de consulta 4', 'María López', 4),
( 'Historial de consulta 5', 'José Martínez', 5);



-- Inserciones en la tabla Status
INSERT INTO Status ( StatusAppoiment, AppoimentId) VALUES
( 'Pendiente', 1),
( 'Completado', 2),
( 'Cancelado', 3),
( 'En progreso', 4),
( 'Reprogramado', 5);


-- Inserciones en la tabla MedicalOrders eta tabla esta mala, la estoy corrigiendo :)
INSERT INTO MedicalOrders (Description, Diagnosis, IdMedication, IdAppoiment,MedicationId,IdAppoiment) VALUES
( 'Orden de antibióticos', 'Infección respiratoria', 1, 1, 1, 1),
( 'Orden de analgésicos', 'Dolor crónico', 2, 2, 2, 2),
( 'Orden de radiografía', 'Dolor en el pecho', 3, 3, 3, 3),
( 'Orden de fisioterapia', 'Lesión muscular', 4, 4, 4, 4),
( 'Orden de control', 'Consulta de seguimiento', 5, 5, 5, 5);
	
INSERT INTO MedicalOrders (Description, Diagnosis, MedicationId, IdAppoiment) 
VALUES
('Orden de antibióticos', 'Infección respiratoria', 1, 1),
('Orden de analgésicos', 'Dolor crónico', 2, 2),
('Orden de radiografía', 'Dolor en el pecho', 3, 3),
('Orden de fisioterapia', 'Lesión muscular', 4, 4),
('Orden de control', 'Consulta de seguimiento', 5, 5);


