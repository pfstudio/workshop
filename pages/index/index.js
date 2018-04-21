//index.js
// 请配置服务器Url
const host = 'api service'

Page({
  data: {
    studentId: '16073120',
    name: '毛寅滔',
    records: []
  },
  onLoad() {
    this.refresh()
  },
  signIn() {
    let info = this.data
    wx.request({
      url: `${host}/SignIn`,
      method: 'POST',
      dataType: 'json',
      data: {
        studentId: info.studentId,
        name: info.name
      },
      success: function(res) {
        if (res.data.result) {
          wx.showToast({
            title: '签到成功'
          })
        } else {
          wx.showModal({
            title: '签到失败',
            content: res.data.message
          })
        }
      }
    })
  },
  signOut() {
    let studentId = this.data.studentId
    wx.request({
      url: `${host}/SignOut`,
      method: 'POST',
      dataType: 'json',
      data: {
        studentId: studentId
      },
      success: function(res) {
        if (res.data.result) {
          wx.showToast({
            title: '签退成功'
          })
        } else {
          wx.showModal({
            title: '签退失败',
            content: res.data.message
          })
        }
      }
    })
  },
  refresh() {
    let that = this
    wx.request({
      url: `${host}/Query`,
      method: 'GET',
      dataType: 'json',
      success: function(res) {
        if (res.statusCode == 200) {
          that.setData({ records: res.data })
        }
      }
    })
  }
})
