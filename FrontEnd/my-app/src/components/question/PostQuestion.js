import React, {Component, Fragment} from 'react';
import PropTypes from 'prop-types';
import withStyles from '@material-ui/core/styles/withStyles';
import MyButton from '../../util/MyButton';
// MUI Stuff
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import CircularProgress from '@material-ui/core/CircularProgress';
import AddIcon from '@material-ui/icons/Add';
import CloseIcon from '@material-ui/icons/Close';
import Select from '@material-ui/core/Select';
import Chip from '@material-ui/core/Chip';
import MenuItem from '@material-ui/core/MenuItem';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
// Redux stuff
import {connect} from 'react-redux';
import {postQuestion, clearErrors} from '../../redux/actions/questionActions';
import {getCategories} from '../../redux/actions/dataActions';

const styles = theme => ({
  ...theme,
  submitButton: {
    position: 'relative',
    float: 'right',
    marginTop: 10,
  },
  progressSpinner: {
    position: 'absolute',
  },
  closeButton: {
    position: 'absolute',
    left: '91%',
    top: '6%',
  },
  categories: {
    display: 'flex',
    flexWrap: 'wrap',
  },
  formControl: {
    minWidth: 320,
  },
});

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};

class PostQuestion extends Component {
  state = {
    open: false,
    QuestionTitle: '',
    QuestionContent: '',
    errors: {},
    CategoriesId: [],
  };

  componentDidMount() {
    this.props.getCategories();
  }

  componentWillReceiveProps(nextProps) {
    if (nextProps.UI.errors) {
      this.setState({
        errors: nextProps.UI.errors,
      });
    }
    if (!nextProps.UI.errors && !nextProps.UI.loading) {
      this.setState({body: '', open: false, errors: {}});
    }
  }
  handleOpen = () => {
    this.setState({open: true});
  };
  handleClose = () => {
    this.props.clearErrors();
    this.setState({open: false, errors: {}});
  };
  handleChange = event => {
    this.setState({[event.target.name]: event.target.value});
  };

  handleCateChange = event => {
    this.setState({CategoriesId: event.target.value});
  };

  handleSubmit = event => {
    event.preventDefault();

    const {QuestionTitle, QuestionContent, CategoriesId} = this.state;
    this.props.postQuestion({
      QuestionTitle,
      QuestionContent,
      CategoriesId: CategoriesId.map(item => item.categoryId),
    });
  };
  render() {
    const {errors, CategoriesId} = this.state;
    const {
      classes,
      UI: {loading},
      data: {categories = []},
    } = this.props;

    return (
      <Fragment>
        <MyButton onClick={this.handleOpen} tip="Post a Question!">
          <AddIcon />
        </MyButton>
        <Dialog
          open={this.state.open}
          onClose={this.handleClose}
          fullWidth
          maxWidth="sm"
        >
          <MyButton
            tip="Close"
            onClick={this.handleClose}
            tipClassName={classes.closeButton}
          >
            <CloseIcon />
          </MyButton>
          <DialogTitle>Post a new question</DialogTitle>
          <DialogContent>
            <form onSubmit={this.handleSubmit}>
              <TextField
                id="standard-basic"
                name="QuestionTitle"
                label="Title"
                onChange={this.handleChange}
                fullWidth
              />
              <TextField
                name="QuestionContent"
                type="text"
                label="QUESTION!!"
                multiline
                rows="3"
                placeholder='Start your question with "What", "How", "Why", etc.'
                error={errors.body ? true : false}
                helperText={errors.body}
                className={classes.textField}
                onChange={this.handleChange}
                fullWidth
              />
              <FormControl className={classes.formControl}>
                <InputLabel id="category-name-label">Categories</InputLabel>
                <Select
                  labelid="category-name-label"
                  id="category"
                  multiple
                  value={CategoriesId}
                  onChange={this.handleCateChange}
                  renderValue={selected => (
                    <div className={classes.categories}>
                      {selected.map(item => (
                        <Chip
                          key={item.categoryId}
                          label={item.categoryName}
                          className={classes.chip}
                        />
                      ))}
                    </div>
                  )}
                  MenuProps={MenuProps}
                >
                  {categories.map(ct => (
                    <MenuItem key={ct.categoryId} value={ct}>
                      {ct.categoryName}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                className={classes.submitButton}
                disabled={loading}
              >
                Submit
                {loading && (
                  <CircularProgress
                    size={30}
                    className={classes.progressSpinner}
                  />
                )}
              </Button>
            </form>
          </DialogContent>
        </Dialog>
      </Fragment>
    );
  }
}

PostQuestion.propTypes = {
  postQuestion: PropTypes.func.isRequired,
  getCategories: PropTypes.func.isRequired,
  clearErrors: PropTypes.func.isRequired,
  UI: PropTypes.object.isRequired,
  data: PropTypes.object.isRequired,
};

const mapStateToProps = state => ({
  UI: state.UI,
  data: state.data,
});

export default connect(mapStateToProps, {
  postQuestion,
  clearErrors,
  getCategories,
})(withStyles(styles)(PostQuestion));
