namespace ITI.HMS.Models.Entities.Enums
{
    /// <summary>
    /// Defines the types of details that can be recorded within a patient's medical record.
    /// </summary>
    public enum MedicalRecordType
    {
        /// <summary>
        /// A doctor's diagnosis of the patient's condition
        /// (e.g., Hypertension, Diabetes).
        /// </summary>
        Diagnosis,

        /// <summary>
        /// Medications prescribed by the doctor,
        /// including dosage and duration.
        /// </summary>
        Prescription,

        /// <summary>
        /// Results from laboratory tests
        /// (e.g., blood test, urine analysis).
        /// </summary>
        LabResult,

        /// <summary>
        /// Imaging reports such as X-rays, MRI, or CT scans.
        /// </summary>
        ImagingReport,

        /// <summary>
        /// A structured plan for the patient’s treatment,
        /// which may include procedures, therapies, or lifestyle advice.
        /// </summary>
        TreatmentPlan,

        /// <summary>
        /// Notes documenting the patient’s progress over time,
        /// including follow-up observations.
        /// </summary>
        ProgressNote
    }
}
