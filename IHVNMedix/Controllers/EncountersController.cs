﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHVNMedix.Data;
using IHVNMedix.Models;
using IHVNMedix.Repositories;
using AutoMapper;
using IHVNMedix.DTOs;
using IHVNMedix.Services;
using Microsoft.Extensions.Logging;

namespace IHVNMedix.Controllers
{
    public class EncountersController : Controller
    {
        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper _mapper;
        private readonly IPatientRepository _patientRepository;
        private readonly ISymptomsRepository _symptomRepository;
        private readonly IVitalSignsRepository _vitalSignsRepository;
        private readonly IMedicalDiagnosisService _medicalDiagnosisService;
        private readonly ILogger<DoctorsController> _logger;

        public EncountersController(
            IEncounterRepository encounterRepository, IMapper mapper,
            IPatientRepository patientRepository,
            ISymptomsRepository symptomRepository,
            IVitalSignsRepository vitalSignsRepository,
            IMedicalDiagnosisService medicalDiagnosisService,
            ILogger<DoctorsController> logger)
        {
            _encounterRepository = encounterRepository;
            _mapper = mapper;
            _patientRepository = patientRepository;
            _symptomRepository = symptomRepository;
            _vitalSignsRepository = vitalSignsRepository;
            _medicalDiagnosisService = medicalDiagnosisService;
            _logger = logger;
        }

        // GET: Encounters
        public async Task<IActionResult> Index()
        {
            var encounters = await _encounterRepository.GetAllEncountersAsync();
            //var encounterDtos = _mapper.Map<IEnumerable<EncounterDto>>(encounters);
            var encounterDtos = new List<EncounterDto>();

            foreach (var encounter in encounters)
            {
                // Fetch the patient's name based on PatientId
                var patient = await _patientRepository.GetPatientByIdAsync(encounter.PatientId);

                // Create an EncounterDto and populate it with the patient's name
                var encounterDto = _mapper.Map<EncounterDto>(encounter);
                encounterDto.PatientName = $"{patient.FirstName} {patient.LastName}";

                encounterDtos.Add(encounterDto);
            }
            return View(encounterDtos);
        }



        // GET: Encounters/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDtos = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDtos);
        }

        // GET: Encounters/Create
        public async Task<IActionResult> Create()
        {

            var patients = await _patientRepository.GetAllPatientsAsync();
            ViewBag.PatientId = new SelectList(patients, "Id", "FirstName"); // show anly "FirstName" property
            var encounterDto = new EncounterDto();

            return View(encounterDto);
        }

        // POST: Encounters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,EncounterDate")] Encounter encounter)
        {
            if (ModelState.IsValid)
            {
                await _encounterRepository.AddEncounterAsync(encounter);
                return RedirectToAction(nameof(Index));
            }
            var encounterDto = _mapper.Map<Encounter>(encounter);
            return View(encounterDto);
        }

        // GET: Encounters/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // POST: Encounters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,EncounterDate")] Encounter encounter)
        {
            if (id != encounter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _encounterRepository.UpdateEncounterAsync(encounter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EncounterExistsAsync(encounter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // GET: Encounters/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var encounterDto = _mapper.Map<EncounterDto>(encounter);
            return View(encounterDto);
        }

        // POST: Encounters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _encounterRepository.DeleteEncounterAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EncounterExistsAsync(int id)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(id);
            return encounter !=null;
        }

        public async Task<IActionResult> EnterVitalSigns(int? encounterId)
        {
            if (encounterId == null)
            {
                return NotFound();
            }

            // Fetch the encounter based on the encounterId
            var encounter = await _encounterRepository.GetEncounterByIdAsync(encounterId.Value);

            if (encounter == null)
            {
                return NotFound();
            }

            // Create a ViewModel from DTO for vital signs entry
            var vitalSignsViewModel = new VitalSignsDto
            {
                EncounterId = encounter.Id,
                // Add other properties as needed
            };

            return View(vitalSignsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveVitalSigns(VitalSignsDto vitalSignsViewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the ViewModel data to your VitalSigns model
                var vitalSigns = new VitalSigns
                {
                    EncounterId = vitalSignsViewModel.EncounterId,
                    Temperature = vitalSignsViewModel.Temperature,
                    Systolic = vitalSignsViewModel.Systolic,
                    Diatolic = vitalSignsViewModel.Diatolic,
                    // Map other properties as needed
                };

                // Save the vital signs data to the repository
                await _vitalSignsRepository.AddVitalSignsAsync(vitalSigns);

                return RedirectToAction("Index"); // Redirect to the encounter list
            }

            // If the model state is not valid, return to the entry form with validation errors
            return View("EnterVitalSigns", vitalSignsViewModel);
        }

        public async Task<IActionResult> ViewVitalSigns(int encounterId)
        {
            // I'm retriving vital signs data for the specified encounterId
            var vitalSigns = await _vitalSignsRepository.GetVitalSignsByEncounterIdAsync(encounterId);

            // Let me use a ViewModel to pass the vital signs data to the view, I'll try ViewBag  if viewModel fails
            return View(vitalSigns);
        }

        public async Task<IActionResult> InputSymptoms(int encounterId)
        {
            var encounter = await _encounterRepository.GetEncounterByIdAsync(encounterId);
            var patient = await _patientRepository.GetPatientByIdAsync(encounter.PatientId);
            var symptoms = await _symptomRepository.GetAllSymptomsAsync(); // Fetch available symptoms from the repository

            var viewModel = new EncounterDto
            {
                Id = encounter.Id, // Use Id from EncounterDto
                PatientId = patient.Id,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                EncounterDate = encounter.EncounterDate,
                // Map symptoms directly to SelectedSymptoms in EncounterDto
                SelectedSymptoms = new List<int>(), // Initialize with an empty list, you'll populate this later
                YourListOfSymptoms = symptoms
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Description })
                    .ToList()
            };

            

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> InputSymptoms(EncounterDto viewModel)
        {
            // Enter a access token available when availble - Only for Production use I guess
            string accessToken = "YOUR_ACCESS_TOKEN_HERE";

            // I prefer to fetch patient information from the EncounterDto
            var patient = await _patientRepository.GetPatientByIdAsync(viewModel.PatientId);

            // Let me also fetch available symptoms from the repository
            IEnumerable<Symptoms> symptoms = await _symptomRepository.GetAllSymptomsAsync(); 

            // Prepare a list of SelectListItem for symptoms
            var symptomItems = symptoms.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Description }).ToList();


            // Creating instance of EncounterSymptomsViewModel fromEncounterDTO
            var encounterSymptomsViewModel = new EncounterDto
            {
                Id = viewModel.Id,
                PatientName = $"{patient.FirstName} {patient.LastName}",
                EncounterDate = viewModel.EncounterDate,
                Symptoms = viewModel.Symptoms,
                SelectedSymptoms = viewModel.SelectedSymptoms
            };

            try
            {
                var selectedSymptoms = viewModel.SelectedSymptoms; // List of selected symptom ids

                // Call the LoadDiagnosisAsync method from IMedicalDiagnosisService
                var diagnosisResults = await _medicalDiagnosisService.LoadDiagnosisAsync(
                    selectedSymptoms,
                    patient.Gender,
                    patient.DOB.Year, // Extract the year of birth from patient's Date of Birth
                    accessToken
                );
                ViewData["DiagnosisResults"] = diagnosisResults;
                return View("DiagnosisResultsView", diagnosisResults); 
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during diagnosis
                ModelState.AddModelError("", "An error occurred while fetching diagnosis results. Please try again later.");
                _logger.LogError(ex, "Error fetching diagnosis results.");
            }


            // If an error occurred or the diagnosis failed, return to the input symptoms view
            return View(encounterSymptomsViewModel);
        }

    }
}
