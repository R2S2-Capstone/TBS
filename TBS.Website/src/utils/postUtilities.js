const postUtilities = {
  parsePostStatus: (status) => {
    if (status === 0) {
      return 'Open' 
    } else if (status === 1) {
      return 'Closed'
    } else if (status === 2) {
      return 'Pending Delivery'
    } else if (status === 3) {
      return 'Pending Delivery Approval'
    } else {
      return 'Unknonw'
    }
  },
  parseTrailerType: (type) => {
    if (type === 0) {
      return 'Enclosed' 
    } else if (type === 1) {
      return 'Flat Bed'
    } else if (type === 2) {
      return 'Car Carrier'
    } else if (type === 3) {
      return 'Other'
    } else {
      return 'Unknonw'
    }
  }
}
export default postUtilities