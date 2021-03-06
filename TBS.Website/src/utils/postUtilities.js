const postUtilities = {
  parsePostStatus: (status) => {
    if (status === 0) {
      return 'Closed'
    } else if (status === 1) {
      return 'Open'
    } else if (status === 2) {
      return 'Pending Delivery'
    } else if (status === 3) {
      return 'Pending Delivery Approval'
    } else {
      return 'Unknown'
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
      return 'Unknown'
    }
  },
  reverseParseTrailerType: (type) => {
    if (type == 'Enclosed') {
      return 0
    } else if (type == 'Flat Bed') {
      return 1
    } else if (type == 'Car Carrier') {
      return 2
    } else if (type == 'Other') {
      return 3
    } else {
      return 0
    }
  },
  parseVehicleCondition(condition) {
    if (condition === 0) {
      return 'New' 
    } else if (condition === 1) {
      return 'Used'
    } else {
      return 'Unknown'
    }
  },
  parseDate(date) {
    return new Date(date).toISOString().split('T')[0]
  },
  parseTime(value) {
    if (value === undefined) return
    var hour = value.substring ( 0,2 )
    var minutes = value.substring ( 3,5 )
    var identifier = 'AM'

    if(hour == 12) {
      identifier = 'PM'
    } else if(hour == 0) {
      hour = 12
    } else if(hour > 12) {
      hour = hour - 12
      identifier = 'PM'
    }
    return hour + ':' + minutes + ' ' + identifier;
  },
  formatAddress(address) {
    return `${address.addressLine}, ${address.city}, ${address.province}, ${address.country} ${address.postalCode}`
  },
  formatMoney(amount) {
    return amount.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
  }
}
export default postUtilities