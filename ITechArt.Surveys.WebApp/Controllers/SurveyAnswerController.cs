using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Services.Answers;
using iTechArt.Surveys.Foundation.Services.Surveys;
using iTechArt.Surveys.Foundation.Services.Users;
using iTechArt.Surveys.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Surveys.WebApp.Controllers
{
    public class SurveyAnswerController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly ISurveyAnswersService _surveyAnswersService;
        private readonly IUserService _userService;
        
        
        public SurveyAnswerController(ISurveyService surveyService, ISurveyAnswersService surveyAnswersService,
        IUserService userService)
        {
            _surveyService = surveyService;
            _surveyAnswersService = surveyAnswersService;
            _userService = userService;
        }
        
        
        public async Task<IActionResult> Answer(Guid id)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(id);
            var surveyView = CreateFromSurvey(survey);
            
            return View(surveyView);
        }
        
        [HttpPost]
        public async Task<IActionResult> Answer([FromBody]AnswerViewModel answerViewModel)
        {
            var authorId = User.GetUserId();
            var author = await _userService.GetUserByIdAsync(authorId);

            if (author == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var answer = await CreateFromAnswerViewModel(answerViewModel);

            await _surveyAnswersService.AddAnswerAsync(answer, author);
            
            return Ok();
        }

        private SurveyViewModel CreateFromSurvey(Survey survey)
        {
            var surveyView = new SurveyViewModel()
            {
                Id = survey.Id,
                Name = survey.Name,
                Questions = survey.Questions.Select(question => new QuestionViewModel()
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type,
                    Options = question.Options.Select(option => new QuestionOptionsViewModel()
                    {
                        Id = option.Id,
                        Text = option.Text
                    }).ToList()
                }).ToList()
            };

            return surveyView;
        }
        
        
        private async Task<SurveyAnswer> CreateFromAnswerViewModel(AnswerViewModel answerViewModel)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(answerViewModel.SurveyId);
            
            var answer = new SurveyAnswer()
            {
                SurveyId = answerViewModel.SurveyId,
                QuestionAnswers = answerViewModel.Questions.Select(question =>
                {
                    var answer = new QuestionAnswer()
                    {
                        QuestionId = question.QuestionId
                    };
                    
                    var questionDb = survey.Questions
                        .FirstOrDefault(questionDb => questionDb.Id == question.QuestionId);

                    switch (questionDb?.Type)
                    {
                        case QuestionType.Checkbox or QuestionType.Radio:
                            answer.Options = question.OptionIds.Select(optionId => new QuestionAnswerOption()
                            {
                                QuestionOptionId = optionId
                            }).ToList();
                            break;
                        case QuestionType.Text:
                            answer.TextAnswer = question.TextAnswer;
                            break;
                        case QuestionType.File:
                            var id = Guid.NewGuid();
                            answer.FileAnswerId = id;
                            answer.FileAnswer = new FileAnswer()
                            {
                                Id = id,
                                FileInfo = new FileInfo()
                                {
                                    Id = question.FileAnswer.FileId,
                                    Name = question.FileAnswer.Name,
                                    ContentType = question.FileAnswer.ContentType
                                }
                            };
                            break;
                        case QuestionType.Rate:
                            answer.RateAnswer = question.RateAnswer;
                            break;
                        case QuestionType.Scale:
                            answer.ScaleAnswer = question.ScaleAnswer;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(questionDb.Type), "No such type");
                    }

                    return answer;
                }).ToList()
            };

            return answer;
        }
    }
}