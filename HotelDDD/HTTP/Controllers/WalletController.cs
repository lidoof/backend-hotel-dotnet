using HotelDDD.Domain.Wallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace HotelDDD.HTTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _walletService;

        public WalletController(WalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{walletId}")]
        public async Task<IActionResult> GetBalance(Guid walletId)
        {
            try
            {
                var wallet = await _walletService.GetWalletAsync(walletId);

                if (wallet == null)
                {
                    return NotFound();
                }

                return Ok(new { Balance = wallet.Balance, Currency = wallet.PreferredCurrency });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{walletId}/add-funds")]
        public async Task<IActionResult> AddFunds(Guid walletId, [FromBody] AddFundsRequest request)
        {
            try
            {
                var wallet = await _walletService.GetWalletAsync(walletId);

                if (wallet == null)
                {
                    return NotFound();
                }

                // Utilise l'énumération Currency pour la conversion
                wallet.AddFunds(request.Amount, request.PreferredCurrency);

                // Mettez à jour le portefeuille dans la base de données
                await _walletService.UpdateWalletAsync(wallet);

                return Ok(new  { Balance = wallet.Balance} );
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

    public class AddFundsRequest
    {
        public decimal Amount { get; set; }
        [JsonProperty("PreferredCurrency")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public Currency PreferredCurrency { get; set; }
    }

