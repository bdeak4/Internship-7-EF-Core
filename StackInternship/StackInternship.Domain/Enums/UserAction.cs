using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackInternship.Domain.Enums
{
    public enum UserAction
    {
        CreateComment,
        UpvoteResource,
        DownvoteResource,
        CreateSubComment,
        UpvoteComment,
        DownvoteComment,
        EditComment,
        DeleteComment,
        NoAction
    }
}
