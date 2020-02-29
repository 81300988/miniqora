import React, {Component} from 'react';
import withStyles from '@material-ui/core/styles/withStyles';
import dayjs from 'dayjs';
import relativeTime from 'dayjs/plugin/relativeTime';
import PropTypes from 'prop-types';
import MyButton from '../../util/MyButton';
import LikeButton from './LikeButton';
import CreateComment from './CreateComment';
import Comment from './Comment';
// MUI Stuff
import {Card, Grid, CardContent, Avatar, Typography} from '@material-ui/core';

// Icons
import ChatIcon from '@material-ui/icons/Chat';
// Redux
import {Link} from 'react-router-dom';
import axios from 'axios';

const styles = {
  card: {
    position: 'relative',
    display: 'flex',
    marginBottom: 20,
  },
  image: {
    minWidth: 200,
  },
  content: {
    padding: 25,
    objectFit: 'cover',
    width: '100%',
  },
  mtl_10: {
    marginLeft: 10,
  },
};

class Question extends Component {
  state = {
    showComment: false,
    comments: [],
    totalCommentForAnswer: null,
  };

  render() {
    dayjs.extend(relativeTime);
    const {showComment, comments, totalCommentForAnswer} = this.state;

    const {
      classes,
      question: {
        avatarUrl,
        questionContent,
        questionTitle,
        timeOfAnswer,
        questioner,
        answerId,
        answerContent,
        questionId,
        totalVote,
        totalComment,
        isVotedForAnswer,
      },
    } = this.props;

    const addComment = async CommentContent => {
      axios
        .post('/api/comment/CommentForQuestion', {
          QuestionId: questionId,
          CommentContent,
        })
        .then(res => {
          debugger
          this.setState({
            comments: res.data.comments,
            totalCommentForAnswer: res.data.comments.length
          });
        })
        .catch(err => {
          this.setState({
            comments: [],
          });
        });
    };

    const onClickComment = () => {
      axios
        .get('/api/comment/GetCommentByQuestionId', {
          params: {id: questionId},
        })
        .then(res => {
          this.setState({
            comments: res.data.comments,
          });
        })
        .catch(() => {
          this.setState({
            comments: [],
          });
        });

      this.setState({
        showComment: !showComment,
      });
    };

    return (
      <Card className={classes.card}>
        <CardContent className={classes.content}>
          <Typography
            variant="subtitle1"
            color="primary"
            component={Link}
            to={`/question/${questionId}`}
          >
            {questionTitle}
          </Typography>
          <Grid container>
            <Grid item>
              <Avatar src={`${process.env.REACT_APP_API_URL}${avatarUrl}`} />
            </Grid>
            <Grid item className={classes.mtl_10}>
              <Typography
                className={classes.title}
                color="textSecondary"
                gutterBottom
                variant="body2"
              >
                {questioner}
              </Typography>
              <Typography variant="body2" color="textSecondary">
                {dayjs(timeOfAnswer).fromNow()}
              </Typography>
            </Grid>
          </Grid>
          <Typography variant="body1">{answerContent}</Typography>
          <LikeButton isVote={isVotedForAnswer} answerId={answerId} />
          <span>{totalVote} Votes</span>
          <MyButton tip="comments" onClick={onClickComment}>
            <ChatIcon color="primary" />
          </MyButton>
          <span>{totalCommentForAnswer || totalComment} comments</span>
          {showComment && (
            <>
              <CreateComment addComment={addComment} />
              {(comments || []).map(comment => (
                <Comment comment={comment} key={comment.questionCommentId} />
              ))}
            </>
          )}
        </CardContent>
      </Card>
    );
  }
}

Question.propTypes = {
  question: PropTypes.object.isRequired,
  classes: PropTypes.object.isRequired,
  openDialog: PropTypes.bool,
};

export default withStyles(styles)(Question);
