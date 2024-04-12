using Azure;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace NewsLetter.WebUI.Helpers;

public static class MyHelper
{
    public static void Handle(Controller controller, Result<string> response, out int result, string? status = null)
    {
        result = response.StatusCode;
        if (result == 200)
        {
            controller.TempData["Message"] = response.Data;
            controller.TempData["status"] = status ?? "success";
            result = 200;
        }
        else
        {
            controller.TempData["Message"] = response.ErrorMessages!.First();
            controller.TempData["status"] = "error";
            result = response.StatusCode;
        }
    }
}

//Kullanım şekli
//var status = "success";
//var response = await _mediator.Send(new CreateSubscribeCommand(email));
//ilk yöntem
//MyHelper.Handle(this, response, out result, status);
//ikinci yöntem
//MyHelper.Handle(this,  response, out result, null);
//üçüncü kullanım
//MyHelper.Handle(this, response, out result);

//        if (result == 200)
//        {
//    return RedirectToAction("Index");
//}
//        else
//        {
//    return RedirectToAction("Index", "Home");
//}