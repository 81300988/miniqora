

import React, {Component} from 'react';
import PropTypes from 'prop-types';
import withStyles from '@material-ui/core/styles/withStyles';
import {
  Card,
  Grid,
  CardContent,
  Avatar,
  Typography,
  Divider,
} from '@material-ui/core';

import dayjs from 'dayjs';
import relativeTime from 'dayjs/plugin/relativeTime';

import {connect} from 'react-redux';
import {
  resetCategoryPage,
  getQuestionDetail,
} from '../redux/actions/categoryActions';
import CreateAnswer from '../components/question/CreateAnswer';

import AnswerForQuestionDetail from '../components/question/AnswerForQuestionDetail';

import axios from 'axios';
import MyButton from '../util/MyButton';
import LikeButton from '../components/question/LikeButtonQuestionDetail.js';
// Icons
import ChatIcon from '@material-ui/icons/Chat';
import LikeButtonQuestionDetail from '../components/question/LikeButtonQuestionDetail.js';
const styles = {
  card: {
    display: 'flex',
    flex: 1,
    marginBottom: 20,
  },
  content: {
    padding: 25,
    width: '100%',
  },
  padding: {
    padding: '10px 0',
  },
  commentBody: {
    padding: '10px',
    wordWrap: 'break-word',
    fontSize: '14px',
  },
  cmtWrapper: {
    paddingLeft: '30px',
    paddingTop: '20px',
  },
  commentDate: {
    borderBottom: 'none',
    color: '#9199a1',
  },
  commentUser: {
    whiteSpace: 'nowrap',
    padding: '0',
    color: '#0077cc',
  },
  answers: {
    paddingTop: '10px',
  },
  answersHeader: {
    marginTop: '10px',
  },
  answersCount: {
    fontWeight: '400',
    fontSize: '18px',
  },
  answersBlock: {
    padding: '10px 0',
    borderTop: '1px solid rgba(0, 0, 0, 0.12)',
  },
  mtl_10: {
    marginLeft: 10,
  },
};

class QuestionDetail extends Component {
  state = {
    questionId: '',
    showAddAnswer: false,
    showAddCommentForAnswer: false,
    answersLocal: [],
    count: false,
  };
  
  componentDidMount() {
    const {questionId} = this.props.match.params;

    this.setState({questionId: questionId});
    this.props.getQuestionDetail({questionId});
  }

  componentWillUnmount() {}

  render() {
    const addAnswer = async AnswerContent => {
      axios
        .post('/api/answer/AddAnswer', {
          QuestionId: this.state.questionId,
          AnswerContent,
        })
        .then(res => {
          this.setState({
            answersLocal: res.data.answers,
            count: true,
          });
        })
        .catch(err => {
          this.setState({
            answersLocal: [],
          });
        });
    };
    

    const onClickComment = () => {
      this.setState({
        showAddAnswer: !this.state.showAddAnswer,
      });
    };
    
    dayjs.extend(relativeTime);

    const {classes} = this.props;
    const {answersLocal,count} = this.state;
    const {
      question: {
        questionTitle,
        questionPotster,
        questionContent,
        comments,
        answers = [],
      },
    } = this.props.questionDetail;

    return (
      <Grid container spacing={16}>
        <Card className={classes.card}>
          <CardContent className={classes.content}>
            <div className={classes.padding}>
              <Typography variant="h6" gutterBottom>
                {questionTitle}
              </Typography> 
              <MyButton tip="answers" onClick={onClickComment}>
                <ChatIcon color="primary" />
              </MyButton>
              {this.state.showAddAnswer&&<CreateAnswer addAnswer={addAnswer} />}
              <Typography variant="caption" display="block" gutterBottom>
                by {questionPotster}
              </Typography>
            </div>
            <Divider />
            <div className={classes.padding}>
              <Typography variant="body1" gutterBottom>
                {questionContent}
              </Typography>

              <div className={classes.cmtWrapper}>
                {(comments || []).map(comment => (
                  <div key={comment.questionCommentId}>
                    <Divider />
                    <div className={classes.commentBody}>
                      <span>{comment.commentContent}</span>
                      &nbsp;–&nbsp;
                      <a className={classes.commentUser}>{comment.commenter}</a>
                      &nbsp;
                      <span className={classes.commentDate}>
                        {dayjs(comment.createdDate).fromNow()}
                      </span>
                    </div>
                  </div>
                ))}
              </div>

              <div className={classes.answers}>
                <div className={classes.answersHeader}>
                  <h2 className={classes.answersCount}>
                    {count?answersLocal.length:answers.length} Answers
                  </h2>

                  {!count?((answers || []).map(answer => (
                    <AnswerForQuestionDetail answer = {answer}/>
                  ))):((answersLocal || []).map(answer => (
                    <AnswerForQuestionDetail answer = {answer}/>
                  )))}
                </div>
              </div>
            </div>
          </CardContent>
        </Card>
      </Grid>
    );
  }
}

QuestionDetail.propTypes = {
  classes: PropTypes.object.isRequired,
  getQuestionDetail: PropTypes.func.isRequired,
  resetCategoryPage: PropTypes.func.isRequired,
};

const mapStateToProps = state => {
  return {
    questionDetail: state.questionDetail,
  };
};

export default connect(mapStateToProps, {getQuestionDetail, resetCategoryPage})(
  withStyles(styles)(QuestionDetail)
);
