using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAPI.Recursos.GerarCodigos;

public static class GerarGuids
{

    public static Guid NovoGuid()    {
                
        return Guid.NewGuid();
    }

    public static string NovoGuidBase64()
    {
        string base64Guid = Convert.ToBase64String(NovoGuid().ToByteArray());
		

		return base64Guid;
    }

    public static string GetUUIdBase64()
    {
        string base64GuidUId = NovoGuidBase64();  // string com 24 caracteres
        base64GuidUId = base64GuidUId[..22];       // usara apenas os 22 caracteres

        return base64GuidUId;
    }

	public static string GetUUIdString()
	{
		string UIdString = NovoGuid().ToString(); 
		     

		return UIdString;
	}
}
