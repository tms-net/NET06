using APILibrary.Models;
using FileLibrary;
using System;
using UILibrary;

var uIClient = new UIApplication(new FileService<ShortRate>);

await uIClient.ToDo();