using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Ovning14_SkalProj.Core.Entities;
using Ovning14_SkalProj.Models;

namespace Ovning14_SkalProj.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    //[HtmlTargetElement("tag-name")]
    public class InstructorCardTagHelper : TagHelper
    {
        
        public Instructor Instructor { get; set; } = new Instructor();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string content = $@"<div class='card'>
		                <div class='card-img'><img src='static/speakers/Speaker-{Instructor.InstructorId}.jpg' /></div>
		                <h4 class='card-title'><a href='#'>{Instructor.FirstName} {Instructor.LastName}</a></h4>
		                <p class='card-position'>{Instructor.IsPersonalTrainer}</p>
		                <p class='card-description'>Keynote: {Instructor.Biography}</p>
		                <ul class='social accent-color'>
			                <li>
				                <a target='_blank' href='#'>LinkedIn</a>
			                </li>
			                <li>
				                <a target='_blank' href='#'>Microsoft</a>
			                </li>
		                </ul>
	                </div>";
            output.Attributes.SetAttribute("class", "col-xs-12 col-sm-6 col-md-4 col-lg-3");
            output.TagName = "div";
            output.Content.SetHtmlContent(content);

        }
    }
}
