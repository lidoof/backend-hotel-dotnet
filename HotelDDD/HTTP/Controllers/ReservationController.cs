using HotelDDD.Domain.Customer;
using HotelDDD.Domain.Reservation;
using HotelDDD.Domain.Room;
using HotelDDD.Domain.RoomService;
using HotelDDD.Domain.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace HotelDDD.HTTP.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }
        [HttpPost("create")]
        public async Task<ActionResult<Reservation>> CreateReservation([FromBody] ReservationCreateDto reservationDto)
        {
            try
            {
                var reservationRooms = reservationDto.RoomTypes
                    .Select(roomTypeString =>
                    {
                        if (!Enum.TryParse<RoomType>(roomTypeString, true, out var roomType))
                        {
                            throw new ArgumentException($"Invalid room type: {roomTypeString}");
                        }
                        return new ReservationRoom { RoomType = roomType };
                    })
                    .ToList();

                var reservation = await _reservationService.CreateReservationAsync(
                    reservationDto.CustomerId,
                    reservationRooms,
                    reservationDto.CheckInDate,
                    reservationDto.NumberOfNights
                );
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm/{reservationId}")]
        public async Task<IActionResult> ConfirmReservation(Guid reservationId)
        {
            try
            {
                await _reservationService.ConfirmReservationAsync(reservationId);
                return Ok(new {message= "La réservation  a été confirmée."});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cancel/{reservationId}")]
        public async Task<IActionResult> CancelReservation(Guid reservationId)
        {
            try
            {
                await _reservationService.CancelReservationAsync(reservationId);
                return Ok($"La réservation {reservationId} a été annulée.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ReservationCreateDto
    {
        public Guid CustomerId { get; set; }
        public List<string> RoomTypes { get; set; } // Acceptez RoomType en tant que liste de chaînes
        public DateTime CheckInDate { get; set; }
        public int NumberOfNights { get; set; }
    }

}
