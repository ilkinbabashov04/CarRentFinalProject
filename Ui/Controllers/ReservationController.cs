using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;
using Ui.Helper;

namespace Ui.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v1 = "Rent A Car";
            ViewBag.v2 = "Reservation Form";
            ViewBag.v3 = id;

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7140/api/Location/GetAll");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<LocationDto>>>(jsonData);
            var values = apiResponse?.Data;

            ViewBag.v = values?.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            var pricingResponse = await client.GetAsync("https://localhost:7140/api/Pricing/GetAll");
            if (pricingResponse.IsSuccessStatusCode)
            {
                var pricingJson = await pricingResponse.Content.ReadAsStringAsync();
                var pricingApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PricingDto>>>(pricingJson);
                var pricingList = pricingApiResponse?.Data;

                ViewBag.PricingTypes = pricingList?.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            }

            var reservationResponse = await client.GetAsync($"https://localhost:7140/api/Reservation/GetAllReservationByCarId/{id}");
            if (reservationResponse.IsSuccessStatusCode)
            {
                var reservationJson = await reservationResponse.Content.ReadAsStringAsync();
                var reservationApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetReservationDto>>>(reservationJson);
                var reservations = reservationApiResponse?.Data;

                var reservedRanges = reservations?
                    .Select(r => new
                    {
                        Start = r.PickUpDate.ToString("yyyy-MM-dd"),
                        End = r.DropOffDate.ToString("yyyy-MM-dd")
                    })
                    .ToList();

                ViewBag.ReservedRanges = JsonConvert.SerializeObject(reservedRanges);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            var client = _httpClientFactory.CreateClient();

            var dayDifference = (createReservationDto.DropOffDate - createReservationDto.PickUpDate).Days;

            if (dayDifference == 1)
                createReservationDto.PricingId = 1;
            else if (dayDifference > 1 && dayDifference <= 7)
                createReservationDto.PricingId = 2;
            else if (dayDifference > 20)
                createReservationDto.PricingId = 4;
            else
                createReservationDto.PricingId = 1;

            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7140/api/Reservation/AddReservation", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                var carResponse = await client.GetAsync($"https://localhost:7140/api/Car/GetCarById {createReservationDto.CarId}");
                if (!carResponse.IsSuccessStatusCode)
                {
                    return View(createReservationDto);
                }

                var carJson = await carResponse.Content.ReadAsStringAsync();
                var carApiResponse = JsonConvert.DeserializeObject<ApiResponse<CarDto>>(carJson);
                var car = carApiResponse?.Data;

                if (car == null)
                {
                    return View(createReservationDto);
                }

                var pricingResponse = await client.GetAsync($"https://localhost:7140/api/Pricing/GetPricingByCarId?id={createReservationDto.CarId}");
                if (!pricingResponse.IsSuccessStatusCode)
                {
                    return View(createReservationDto);
                }

                var pricingJson = await pricingResponse.Content.ReadAsStringAsync();
                var pricingApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetPricingByCarIdDto>>>(pricingJson);
                var pricingList = pricingApiResponse?.Data;

                var selectedPricing = pricingList?.FirstOrDefault(x => x.PricingId == createReservationDto.PricingId);
                if (selectedPricing == null)
                {
                    return View(createReservationDto);
                }

                var priceAmount = selectedPricing.Amount;

                // ✅ Send confirmation email
                SendEmail(
                    toEmail: createReservationDto.Mail,
                    fullName: createReservationDto.Name,
                    carModel: car.Model,
                    pickUp: createReservationDto.PickUpDate,
                    dropOff: createReservationDto.DropOffDate,
                    amount: priceAmount
                );

                TempData["carName"] = car.Model;
                TempData["amount"] = priceAmount.ToString();
                return RedirectToAction("Checkout", "Payment");
            }

            var locationResponse = await client.GetAsync("https://localhost:7140/api/Location/GetAll");
            if (locationResponse.IsSuccessStatusCode)
            {
                var locationJson = await locationResponse.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<LocationDto>>>(locationJson);
                var values = apiResponse?.Data;

                ViewBag.v = values?
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    })
                    .ToList();
            }

            return View(createReservationDto);
        }

        private void SendEmail(string toEmail, string fullName, string carModel, DateTime pickUp, DateTime dropOff, decimal amount)
        {
            var fromAddress = new MailAddress("ilkinbabasov99@gmail.com", "BookCar");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "okszylulgpnlongo"; // Use app password for Gmail
            const string subject = "Your Reservation Confirmation";

            string body = $@"
Hello {fullName},

Your reservation has been successfully created.

Car: {carModel}
Pickup Date: {pickUp:dd.MM.yyyy}
Drop-off Date: {dropOff:dd.MM.yyyy}
Total Price: ${amount}

Thank you for choosing us!

Best regards,
BookCar Team
";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // Your SMTP host
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            smtp.Send(message);
        }
    }
}
